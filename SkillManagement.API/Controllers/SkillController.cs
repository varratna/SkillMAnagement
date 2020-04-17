using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;
using SkillManagement.API.Services;

namespace SkillManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private ISkillService _skillService;
        public ILoggingService _logger;

        public SkillController(ISkillService skillService, ILoggingService logger)
        {
            _skillService = skillService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //throw new Exception("Exception while fetching all the user from the storage.");
                _logger.LogInfo("In Get");

                var skills = _skillService.GetAll();
                if (skills == null)
                {
                    _logger.LogInfo("No Skills Fetched");
                }
                else
                {
                    _logger.LogInfo(skills.Count().ToString() + "Skills Fetched");
                    


                }

                return Ok(skills);
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

                Skill skill = _skillService.Get(id);

                if (skill == null)
                {
                    _logger.LogInfo("skill not present with id" + id);
                    return NotFound("The Skill record couldn't be found.");
                }

                return Ok(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Skill skill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _skillService.Add(skill);

                    if (skill.Id > 0)
                    {
                        return Ok(skill);
                    }
                    else
                    {
                        _logger.LogInfo("skill not saved");
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
        public IActionResult Put([FromBody] Skill skill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _skillService.Update(skill);
                    return Ok(skill);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // DELETE: api/skill/5
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
                _skillService.Delete(id);
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