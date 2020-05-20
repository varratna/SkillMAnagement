using System.Collections.Generic;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Services
{
    public interface IEmployeeSkillLevelService
    {
        IEnumerable<EmployeeSkillLevel> GetAll();

        IEnumerable<EmployeeSkillLevel> Get(long id);
        void Update(EmployeeSkillLevel entity);

        void Update(IEnumerable<EmployeeSkillLevel> entities);
        void Add(EmployeeSkillLevel entity);
        void Delete(long id);

        void DeleteByEmployeeId(long userId);

        IEnumerable<EmployeeSkillLevel> GetByEmployeeId(long userId);
    }
}
