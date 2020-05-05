using System;
using System.Collections.Generic;
using System.Text;

namespace SkillManagement.API.Core.Models
{
    public abstract class BaseEntityClass
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
