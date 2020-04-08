using System.ComponentModel.DataAnnotations;

namespace SkillManagement.API.Core.Models
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
