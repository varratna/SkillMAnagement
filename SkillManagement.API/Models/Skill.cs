using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillManagement.API.Models
{
    public class Skill
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string SkillName { get; set; }
        public string Description { get; set; }

        public IList<UserSkillLevel> UserSkillLevel { get; set; }
    }
}
