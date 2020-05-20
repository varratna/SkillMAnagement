using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;

namespace SkillManagement.API.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private SkillContext context;

        public EmployeeRepository(SkillContext context)
          : base(context)
        {
            this.context = context;

        }

        public new IEnumerable<Employee> GetAll()
        {
            return context.Employees.Include(a=>a.EmployeeRoles).ToListAsync().Result;
        }
    }
}
