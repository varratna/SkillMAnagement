using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;
using SkillManagement.API.ViewModel;

namespace SkillManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public ILoggingService _logger;

        public UsersController(IUserService userService, ILoggingService logger)
        {

            _userService = userService;
            _logger = logger;


        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //throw new Exception("Exception while fetching all the user from the storage.");

                _logger.LogInfo("In Get");

                var users = _userService.GetAll();
                if (users == null)
                {
                    _logger.LogInfo("No User Fetched");
                }
                else
                {
                    _logger.LogInfo("Users Fetched");
                }

                return Ok(GetUserViewModel(users));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInfo("paramter to get request is 0 or null");
                    return BadRequest();
                }

                User user = _userService.Get(id);

                if (user == null)
                {
                    _logger.LogInfo("User not present with id" + id);
                    return NotFound("The User record couldn't be found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        // POST: api/employee
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    bool IsEmailPresent = IsEmailIdPresent(user.Email);
                    if (IsEmailPresent)
                    {
                        _logger.LogInfo(user.Email + " is already present");
                        return Content("Email already present");
                    }
                    //user.Password = EncodePasswordToBase64(user.Password);

                    _userService.Add(user);

                    if (user.Id > 0)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        _logger.LogInfo("User not saved");
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
        public IActionResult Put([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //user.Password = EncodePasswordToBase64(user.Password);
                    _userService.Update(user);


                    return Ok(user);
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
                _userService.Delete(id);
                return Ok("User with ID " + id + " deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var token = _userService.Authenticate(userParam.UserName,  userParam.Password);

            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new { token });
        }

        private List<UserViewModel> GetUserViewModel(IEnumerable<User> users)
        {
            List<UserViewModel> lstUsers = new List<UserViewModel>();
            foreach (var user in users)
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.Id = user.Id;
                userViewModel.Email = user.Email;
                userViewModel.UserName = user.UserName;
                userViewModel.Password = user.Password;
                lstUsers.Add(userViewModel);
            }
            return lstUsers;
        }

        public bool IsEmailIdPresent(string EmailId)
        {

            var emailPresent = _userService.GetAll().Where(m => m.Email == EmailId).FirstOrDefault();
            if (emailPresent != null)
                return true; //Json("Email already exist", JsonRequestBehavior.AllowGet);
            return false;



        }

        

      
    }
}