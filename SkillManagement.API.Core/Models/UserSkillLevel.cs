using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillManagement.API.Core.Models
{
    public class UserSkillLevel
    {
        public long Id { get; set; }
        [Required]
        
        public long UserId { get; set; }
        public User User { get; set; }
        [Required]
        
        public long SkillId { get; set; }
        public virtual Skill Skill { get; set; }
        [Required]
        
        public long LevelId { get; set; }
        public virtual Level Level { get; set; }

    }
}
