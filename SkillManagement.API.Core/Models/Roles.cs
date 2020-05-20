using System;
using System.Collections.Generic;
using System.Text;

namespace SkillManagement.API.Core.Models
{
    public class Roles :BaseEntityClass
    {
        public long Id { get; set; }

        public string Role { get; set; }

        public  virtual IList<EmployeeRoles> EmployeeRoles { get; set; }
    }
}
