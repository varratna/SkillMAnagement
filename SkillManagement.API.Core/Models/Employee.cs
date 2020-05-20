using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillManagement.API.Core.Models
{
    public class Employee:BaseEntityClass
    {
        public long Id { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "First Name cannot be empty")]
        //[MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Email Address cannot be empty")]
        //[Display(Name = "EmailID")]
        //[Remote("IsEmailIdPresent", "User",ErrorMessage ="Email already present")]
        public string EmailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password requireds")]
        public string Password { get; set; }

        public string Token { get; set; }

        public virtual IList<EmployeeRoles> EmployeeRoles { get; set; }


        public IList<EmployeeSkillLevel> EmployeeSkillLevel { get; set; }

        
    }
}
