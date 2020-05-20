using System.Collections.Generic;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Employee Get(long id);
        void Update(Employee entity);
        void Add(Employee entity);
        void Delete(long id);

        Employee Authenticate(string username, string password);


    }
}
