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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeSkillLevelController : ControllerBase
    {
        private IEmployeeSkillLevelService _employeeSkillLevelService;
        public ILoggingService _logger;

        public EmployeeSkillLevelController(IEmployeeSkillLevelService employeeSkillLevelService, ILoggingService logger)
        {
            _employeeSkillLevelService = employeeSkillLevelService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {               

                //throw new Exception("Exception while fetching all the user from the storage.");
                _logger.LogInfo("In Get");

                var employeeSkillLevel = _employeeSkillLevelService.GetAll();
                if (employeeSkillLevel == null)
                {
                    _logger.LogInfo("No Employee Skill Level Fetched");
                }
                else
                {
                    _logger.LogInfo("Employee Skill Level Fetched");
                }

                return Ok(GetEmployeeSkillLevelViewModel(employeeSkillLevel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{employeeid}")]
        public IActionResult Get(long employeeid)
        {
            try
            {
                if (employeeid == 0)
                {
                    _logger.LogInfo("paramter to get request is 0 or null");
                    return BadRequest();
                }

                var employeeSkillLevels = _employeeSkillLevelService.GetByEmployeeId(employeeid);

                if (employeeSkillLevels == null)
                {
                    _logger.LogInfo("Employee Skill level not present with employee id" + employeeid);
                    return NotFound("The Employee Skill level record couldn't be found.");
                }



                return Ok(GetEmployeeSkillLevelViewModel(employeeSkillLevels));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/employeeSkillLevel
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeSkillLevel employeeSkillLevel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeSkillLEvelEXists = _employeeSkillLevelService.GetAll().Where(m => m.EmployeeId == employeeSkillLevel.EmployeeId &&
                    m.SkillId == employeeSkillLevel.SkillId).FirstOrDefault();
                    if (employeeSkillLEvelEXists != null)
                    {
                        return BadRequest("Skill for employee already exists");
                    }

                    _employeeSkillLevelService.Add(employeeSkillLevel);

                    if (employeeSkillLevel.EmployeeId > 0)
                    {
                        return Ok(employeeSkillLevel);
                    }
                    else
                    {
                        _logger.LogInfo("EmployeeSkillLevel not saved");
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
        public IActionResult Put([FromBody] EmployeeSkillLevel[] employeeSkillLevel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeSkillLevelService.Update(employeeSkillLevel);
                    return Ok(employeeSkillLevel);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }

            return BadRequest();
        }


        [HttpPut("{employeeSkillLevels}")]
        public IActionResult Put([FromBody] List<EmployeeSkillLevel> employeeSkillLevels)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeSkillLevelService.Update(employeeSkillLevels);
                    return Ok(employeeSkillLevels);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // DELETE: api/employeeskilllevel/5


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
                _employeeSkillLevelService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // DELETE: api/employeeskilllevel/5
        // by user is
        [HttpDelete("{employeeId}")]

        public IActionResult DeleteByUserId(long employeeId)
        {
            if (employeeId == 0)
            {
                _logger.LogInfo("employeeId is 0 or null");
                return BadRequest();
            }
            try
            {
                _employeeSkillLevelService.DeleteByEmployeeId(employeeId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        private List<EmployeeSkillLevelViewModel> GetEmployeeSkillLevelViewModel(IEnumerable<EmployeeSkillLevel> employeeSkillLevels)
        {
            List<EmployeeSkillLevelViewModel> lstEmployeeSkillLevels = new List<EmployeeSkillLevelViewModel>();
            foreach (var employeeSkillLevel in employeeSkillLevels)
            {
                EmployeeSkillLevelViewModel employeeSkillLevelViewModel = new EmployeeSkillLevelViewModel();
                employeeSkillLevelViewModel.Id = employeeSkillLevel.Id;

                employeeSkillLevelViewModel.EmployeeId = employeeSkillLevel.EmployeeId;
                employeeSkillLevelViewModel.FirstName = employeeSkillLevel.Employee?.FirstName;
                employeeSkillLevelViewModel.LastName = employeeSkillLevel.Employee?.LastName;
                employeeSkillLevelViewModel.EmailId = employeeSkillLevel.Employee?.EmailId; ;

                employeeSkillLevelViewModel.SkillId = employeeSkillLevel.SkillId;
                employeeSkillLevelViewModel.SkillName = employeeSkillLevel.Skill?.SkillName;
                employeeSkillLevelViewModel.SkillDescription = employeeSkillLevel.Skill?.Description;



                employeeSkillLevelViewModel.LevelId = employeeSkillLevel.LevelId;
                employeeSkillLevelViewModel.LevelName = employeeSkillLevel.Level?.LevelName;
                employeeSkillLevelViewModel.LevelDescription = employeeSkillLevel.Level?.Description;

                lstEmployeeSkillLevels.Add(employeeSkillLevelViewModel);
            }
            return lstEmployeeSkillLevels;
        }
    }
}