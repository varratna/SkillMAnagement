using System;
using System.Collections.Generic;
using System.Text;

namespace SkillManagement.API.Core.Models
{
    public class EmployeeRoles:BaseEntityClass
    {
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
        

        public long RoleId { get; set; }
        public virtual  Roles Roles { get; set; }
        
    }
}
