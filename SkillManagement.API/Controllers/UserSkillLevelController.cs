using System;
using LoggingService;
using Microsoft.AspNetCore.Mvc;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;

namespace SkillManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkillLevelController : ControllerBase
    {
        private IUserSkillLevelService _userSkillLevelService;
        public ILoggingService _logger;

        public UserSkillLevelController(IUserSkillLevelService userSkillLevelService, ILoggingService logger)
        {
            _userSkillLevelService = userSkillLevelService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //throw new Exception("Exception while fetching all the user from the storage.");
                _logger.LogInfo("In Get");

                var levels = _userSkillLevelService.GetAll();
                if (levels == null)
                {
                    _logger.LogInfo("No User Skill Level Fetched");
                }
                else
                {
                    _logger.LogInfo("User Skill Level Fetched");
                }

                return Ok(levels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{userid}")]
        public IActionResult Get(long userid)
        {
            try
            {
                if (userid == 0)
                {
                    _logger.LogInfo("paramter to get request is 0 or null");
                    return BadRequest();
                }

                var userSkillLevel = _userSkillLevelService.GetByUserId(userid);

                if (userSkillLevel == null)
                {
                    _logger.LogInfo("userSkillLevel not present with user id" + userid);
                    return NotFound("The userSkillLevel record couldn't be found.");
                }

                return Ok(userSkillLevel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/UserSkillLevel
        [HttpPost]
        public IActionResult Post([FromBody] UserSkillLevel userSkillLevel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userSkillLevelService.Add(userSkillLevel);

                    if (userSkillLevel.UserId > 0)
                    {
                        return Ok(userSkillLevel.UserId);
                    }
                    else
                    {
                        _logger.LogInfo("userSkillLevel not saved");
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
        public IActionResult Put([FromBody] UserSkillLevel userSkillLevel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userSkillLevelService.Update(userSkillLevel);
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

        // DELETE: api/userskilllevel/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                _logger.LogInfo("id paaram is 0");
                return BadRequest();
            }

            try
            {
                _userSkillLevelService.DeleteByUserId(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}