using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillManagement.API.Models;
using SkillManagement.API.Models.Repositories;
using SkillManagement.API.Services;

namespace SkillManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private ILevelService _levelService;
        public ILoggingService _logger;

        public LevelController(ILevelService levelService, ILoggingService logger)
        {
            _levelService = levelService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //throw new Exception("Exception while fetching all the user from the storage.");
                _logger.LogInfo("In Get");

                var levels = _levelService.GetAll();
                if (levels == null)
                {
                    _logger.LogInfo("No Levels Fetched");
                }
                else
                {
                    _logger.LogInfo("Levels Fetched");
                }

                return Ok(levels);
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

                Level level = _levelService.Get(id);

                if (level == null)
                {
                    _logger.LogInfo("level not present with id" + id);
                    return NotFound("The Level record couldn't be found.");
                }

                return Ok(level);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Level level)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _levelService.Add(level);

                    if (level.Id > 0)
                    {
                        return Ok(level.Id);
                    }
                    else
                    {
                        _logger.LogInfo("level not saved");
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
        public IActionResult Put([FromBody] Level level)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _levelService.Update(level);
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

        // DELETE: api/Level/5
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
                _levelService.Delete(id);
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