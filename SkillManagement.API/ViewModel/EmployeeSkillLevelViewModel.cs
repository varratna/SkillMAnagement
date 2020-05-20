using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillManagement.API.ViewModel
{
    public class EmployeeSkillLevelViewModel
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }

        public long SkillId { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
        public long LevelId { get; set; }
        public string LevelName { get; set; }
        public string LevelDescription { get; set; }


    }
}
