using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillManagement.API.Models;
using SkillManagement.API.Models.Repositories;
using SkillManagement.API.Services;

namespace SkillManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        public ILoggingService _logger;


        public UserController(IUserService userService, ILoggingService logger)
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
                    _logger.LogInfo("No Users Fetched");
                }
                else
                {
                    _logger.LogInfo("Users Fetched");
                }

                return Ok(users);
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
                    _logger.LogInfo("user not present with id" + id);
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

        // POST: api/user
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool IsEmailPresent = IsEmailIdPresent(user.EmailId);
                    if (IsEmailPresent)
                    {
                        _logger.LogInfo(user.EmailId + " is already present");
                        return Content("Email already present");
                    }

                    _userService.Add(user);

                    if (user.Id > 0)
                    {
                        return Ok(user.Id);
                    }
                    else
                    {
                        _logger.LogInfo("user not saved");
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
                    _userService.Update(user);


                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // DELETE: api/user/5
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


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


        public bool IsEmailIdPresent(string EmailId)
        {

            var emailPresent = "aa";// _unitOfWork.UserRepository.GetAll().Where(m => m.EmailId == EmailId).FirstOrDefault();
            if (emailPresent != null)
                return true; // Json("Email already exist", JsonRequestBehavior.AllowGet);
            return false;// Json(true, JsonRequestBehavior.AllowGet);



        }
    }
}