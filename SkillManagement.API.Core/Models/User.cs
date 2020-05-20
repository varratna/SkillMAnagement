using System;
using System.Collections.Generic;
using System.Text;

namespace SkillManagement.API.Core.Models
{
    public class User:BaseEntityClass
    {
        public long Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual IList<User_Roles> User_Roles { get; set; }

    }
}
