using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillManagement.API.Models
{
    public class UserSkillLevel
    {
        [Required]
        public long UserId { get; set; }
        public User User { get; set; }
        [Required]
        public long SkillId { get; set; }
        public Skill Skill { get; set; }
        [Required]
        public long LevelId { get; set; }
        public Level Level { get; set; }
    }
}
