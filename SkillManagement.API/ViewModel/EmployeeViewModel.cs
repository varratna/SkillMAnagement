using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillManagement.API.ViewModel
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }      
        
        //[Remote("IsEmailIdPresent", "User",ErrorMessage ="Email already present")]
        public string EmailId { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }
    }
}
