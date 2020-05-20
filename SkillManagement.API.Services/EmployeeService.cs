using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;

namespace SkillManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ApplicationSettings _appSettings;

        public EmployeeService(IUnitOfWork unitOfWork, IOptions<ApplicationSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;

        }

        public void Add(Employee entity)
        {

            entity.Password = EncodePasswordToBase64(entity.Password);
            _unitOfWork.EmployeeRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            _unitOfWork.EmployeeRepository.Delete(id);
            _unitOfWork.Save();
        }

        public Employee Get(long id)
        {
            var user = _unitOfWork.EmployeeRepository.Get(id);
            return user;
        }

        public IEnumerable<Employee> GetAll()
        {
            var users = _unitOfWork.EmployeeRepository.GetAll();
            return users;
        }

        public void Update(Employee entity)
        {
            var user = _unitOfWork.EmployeeRepository.Get(entity.Id);
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.EmailId = entity.EmailId;
            user.UserName = entity.UserName;
            entity.Password = EncodePasswordToBase64(entity.Password);

            _unitOfWork.EmployeeRepository.Update(user);
            _unitOfWork.Save();
        }

        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string DecodePasswordFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public Employee Authenticate(string username, string password)//User
        {
            var employee = _unitOfWork.EmployeeRepository.GetAll().SingleOrDefault(x => x.UserName == username && DecodePasswordFrom64(x.Password) == password);
            
            // return null if user not found
            if (employee == null)
                return null;

            var employeeRoleIds = employee.EmployeeRoles.Select(r => (long)r.RoleId).ToList();
            var roles = _unitOfWork.RoleRepository.GetRoles(employeeRoleIds.ToArray());//.Where(r => userRoleIds.Contains(r.Id));


            var role = employee.EmployeeRoles.FirstOrDefault().Roles;
            // authentication successful so generate jwt token
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWT_Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, employee.Id.ToString()),
                    new Claim(ClaimTypes.Role, role.Id.ToString())
                }),
                //Expires = DateTime.UtcNow.AddDays(7),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenToBeSent = tokenHandler.WriteToken(token);
            //user.Token = tokenHandler.WriteToken(token);
            // remove password before returning
            //user.Password = null;

            employee.Token = tokenToBeSent.ToString();
            employee.Password = null;

            return employee ;
            //return tokenToBeSent;
        }
    }

    
}

