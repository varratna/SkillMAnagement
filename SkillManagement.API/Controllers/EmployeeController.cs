using System;
using System.Collections.Generic;
using System.Linq;
using LoggingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;
using SkillManagement.API.ViewModel;

namespace SkillManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private IEmployeeService _employeeService;
        public ILoggingService _logger;


        public EmployeeController(IEmployeeService employeeService, ILoggingService logger)
        {

            _employeeService = employeeService;
            _logger = logger;


        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //throw new Exception("Exception while fetching all the employee from the storage.");

                _logger.LogInfo("In Get");

                var employees = _employeeService.GetAll();
                if (employees == null)
                {
                    _logger.LogInfo("No Employees Fetched");
                }
                else
                {
                    _logger.LogInfo("Employees Fetched");
                }

                return Ok(GetEmployeeViewModel(employees));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }
               

        // POST: api/employee
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            bool IsEmailPresent = false;
            bool IsUserPresent = false;
            if (ModelState.IsValid)
            {
                try
                {

                    if (!String.IsNullOrWhiteSpace(employee.EmailId))
                    {
                        IsEmailPresent = IsEmailIdPresent(employee.EmailId);
                        if (IsEmailPresent)
                        {
                            _logger.LogInfo(employee.EmailId + " is already present");
                            return Ok("Email already present");
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(employee.UserName))
                    {
                        IsUserPresent = IsUserNamePresent(employee.UserName);
                        if (IsUserPresent)
                        {
                            _logger.LogInfo(employee.UserName + " is already present");
                            return Ok("UserName already present");
                        }
                    }

                    employee.EmployeeRoles = new List<EmployeeRoles>();
                    employee.EmployeeRoles.Add(new EmployeeRoles { RoleId = 2 });


                    
                    _employeeService.Add(employee);

                    if (employee.Id > 0)
                    {
                        IList<Employee> emp = new List<Employee>();
                        emp.Add(employee);
                        return Ok(GetEmployeeViewModel(emp));
                    }
                    else
                    {
                        _logger.LogInfo("Employee not saved");
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }

            }

            return BadRequest();

        }

        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.Update(employee);


                    return Ok(employee);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            long result = 0;

            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                _employeeService.Delete(id);
                return Ok("Employee with ID " + id + " deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


        public bool IsEmailIdPresent(string EmailId)
        {

            var emailPresent = _employeeService.GetAll().Where(m => m.EmailId == EmailId).FirstOrDefault();
            if (emailPresent != null)
                return true; //Json("Email already exist", JsonRequestBehavior.AllowGet);
            return false;
        }

        public bool IsUserNamePresent(string userName)
        {

            var userNamePresent = _employeeService.GetAll().Where(m => m.UserName == userName).FirstOrDefault();
            if (userNamePresent != null)
                return true; 
            return false;
        }

        private List<EmployeeViewModel> GetEmployeeViewModel(IEnumerable<Employee> employees)
        {
            List<EmployeeViewModel> lstEmployees = new List<EmployeeViewModel>();
            foreach (var employee in employees)
            {
                EmployeeViewModel employeevm = new EmployeeViewModel();
                employeevm.Id = employee.Id;
                employeevm.FullName = employee.FirstName + " " + employee.LastName;
                employeevm.FirstName = employee.FirstName;
                employeevm.LastName = employee.LastName;
                employeevm.EmailId = employee.EmailId;
                employeevm.UserName = employee.UserName;


                

                lstEmployees.Add(employeevm);
            }
            return lstEmployees;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Employee userParam)
        {
            var employee = _employeeService.Authenticate(userParam.UserName, userParam.Password);

            if (employee == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new { token= employee.Token,employeeId = employee.Id  ,role = employee.EmployeeRoles.FirstOrDefault().Roles.Role});
        }

    }
}