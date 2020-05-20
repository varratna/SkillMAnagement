using System;
using System.Collections.Generic;
using System.Text;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Repositories
{
    public interface IEmployeeSkillLevelRepository : IGenericRepository<EmployeeSkillLevel>
    {
        IEnumerable<EmployeeSkillLevel> GetByEmployeeId(long userId);
        void DeleteByEmployeeId(long userId);

        void Update(IEnumerable<EmployeeSkillLevel> entities);
    }
}
