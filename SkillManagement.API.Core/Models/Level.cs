﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillManagement.API.Core.Models
{
    public class Level:BaseEntityClass
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string LevelName { get; set; }
        public string Description { get; set; }
        public IList<EmployeeSkillLevel> EmployeeSkillLevel { get; set; }
    }
}
