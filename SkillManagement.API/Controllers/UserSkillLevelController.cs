using System;
using System.Collections.Generic;
using System.Linq;
using LoggingService;
using Microsoft.AspNetCore.Mvc;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;
using SkillManagement.API.ViewModel;

namespace SkillManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
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

                var userskillLevel = _userSkillLevelService.GetAll();
                if (userskillLevel == null)
                {
                    _logger.LogInfo("No User Skill Level Fetched");
                }
                else
                {
                    _logger.LogInfo("User Skill Level Fetched");
                }

                return Ok(GetUSerSkillLevelViewModel(userskillLevel));
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

                var userSkillLevels = _userSkillLevelService.GetByUserId(userid);

                if (userSkillLevels == null)
                {
                    _logger.LogInfo("userSkillLevel not present with user id" + userid);
                    return NotFound("The userSkillLevel record couldn't be found.");
                }



                return Ok(GetUSerSkillLevelViewModel(userSkillLevels));
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
                    var userSkillLEvelEXists = _userSkillLevelService.GetAll().Where(m => m.UserId == userSkillLevel.UserId &&
                    m.SkillId == userSkillLevel.SkillId).FirstOrDefault();
                    if (userSkillLEvelEXists != null)
                    {
                        return BadRequest("Skill for user already exists");
                    }

                    _userSkillLevelService.Add(userSkillLevel);

                    if (userSkillLevel.UserId > 0)
                    {
                        return Ok(userSkillLevel);
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
        public IActionResult Put([FromBody] UserSkillLevel[] userSkillLevel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userSkillLevelService.Update(userSkillLevel);
                    return Ok(userSkillLevel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }

            return BadRequest();
        }


        [HttpPut("{userSkillLevels}")]
        public IActionResult Put([FromBody] List<UserSkillLevel> userSkillLevels)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userSkillLevelService.Update(userSkillLevels);
                    return Ok(userSkillLevels);
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
        public IActionResult DeleteById(long id)
        {
            if (id == 0)
            {
                _logger.LogInfo("id param is 0");
                return BadRequest();
            }
            try
            {
                _userSkillLevelService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // DELETE: api/userskilllevel/5
        // by user is
        [HttpDelete("{userId}")]

        public IActionResult DeleteByUserId(long userId)
        {
            if (userId == 0)
            {
                _logger.LogInfo("userId is 0 or null");
                return BadRequest();
            }
            try
            {
                _userSkillLevelService.DeleteByUserId(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        private List<UserSkillLevelViewModel> GetUSerSkillLevelViewModel(IEnumerable<UserSkillLevel> userSkillLevels)
        {
            List<UserSkillLevelViewModel> lstUserSkillLevels = new List<UserSkillLevelViewModel>();
            foreach (var userSkillLevel in userSkillLevels)
            {
                UserSkillLevelViewModel userSkillLevelViewModel = new UserSkillLevelViewModel();
                userSkillLevelViewModel.Id = userSkillLevel.Id;

                userSkillLevelViewModel.UserId = userSkillLevel.UserId;
                userSkillLevelViewModel.FirstName = userSkillLevel.User?.FirstName;
                userSkillLevelViewModel.LastName = userSkillLevel.User?.LastName;
                userSkillLevelViewModel.EmailId = userSkillLevel.User?.EmailId; ;

                userSkillLevelViewModel.SkillId = userSkillLevel.SkillId;
                userSkillLevelViewModel.SkillName = userSkillLevel.Skill?.SkillName;
                userSkillLevelViewModel.SkillDescription = userSkillLevel.Skill?.Description;



                userSkillLevelViewModel.LevelId = userSkillLevel.LevelId;
                userSkillLevelViewModel.LevelName = userSkillLevel.Level?.LevelName;
                userSkillLevelViewModel.LevelDescription = userSkillLevel.Level?.Description;

                lstUserSkillLevels.Add(userSkillLevelViewModel);
            }
            return lstUserSkillLevels;
        }
    }
}