﻿using System;
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
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ApplicationSettings _appSettings;

        public UserService(IUnitOfWork unitOfWork,IOptions<ApplicationSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;

        }
        public void Add(User entity)
        {
            entity.Password = EncodePasswordToBase64(entity.Password);
            _unitOfWork.UserRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.Save();
        }

        public User Get(long id)
        {
            var user = _unitOfWork.UserRepository.Get(id);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return users;
        }   

        public void Update(User entity)
        {
            var user = _unitOfWork.UserRepository.Get(entity.Id);
            user.UserName = entity.UserName;
            user.Email = entity.Email;
            user.Password = EncodePasswordToBase64(entity.Password);

            _unitOfWork.UserRepository.Update(user);
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

        public string Authenticate(string username, string password)//User
        {

            var user = _unitOfWork.UserRepository.GetAll().SingleOrDefault(x => x.UserName == username && DecodePasswordFrom64(x.Password) ==  password);
                                 

            // return null if user not found
            if (user == null)
                return null;

            var userRoleIds = user.User_Roles.Select(r => (long)r.RoleId).ToList();
            var roles = _unitOfWork.RoleRepository.GetRoles(userRoleIds.ToArray());//.Where(r => userRoleIds.Contains(r.Id));


            var role = user.User_Roles.FirstOrDefault().Roles;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWT_Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
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

            return tokenToBeSent;
        }



        [HttpGet]
        [Authorize (Roles = "Admin")]
        [Route("ForAdmin") ]
        public string GetForAdmin()
        {
            return "Web method for admin ";
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("ForUser")]
        public string GetForUser()
        {
            return "Web method for user";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [Route("ForAdminCutomer")]
        public string GetForAdminCutomer()
        {
            return "Web method for AdminCutomer ";
        }

    }
}
