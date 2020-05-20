using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;

namespace SkillManagement.API.Data.Repositories
{
    public class EmployeeSkillLevelRepository : GenericRepository<EmployeeSkillLevel>, IEmployeeSkillLevelRepository
    {
        private SkillContext context;

        public EmployeeSkillLevelRepository(SkillContext context)
          : base(context)
        {
            this.context = context;

        }

        public new IEnumerable<EmployeeSkillLevel> GetAll()
        {
            return context.EmployeeSkillLevels.Include(a => a.Employee).ThenInclude(a => a.EmployeeSkillLevel).Include(a => a.Skill).ThenInclude(a => a.EmployeeSkillLevel).Include(a => a.Level).ToListAsync().Result;
        }

        public void DeleteByEmployeeId(long userId)
        {
            var entitiesToDelete = context.EmployeeSkillLevels.ToListAsync().Result.FindAll(x => x.EmployeeId == userId);
            context.EmployeeSkillLevels.RemoveRange(entitiesToDelete);
        }

        public IEnumerable<EmployeeSkillLevel> GetByEmployeeId(long userId)
        {

            var users = context.EmployeeSkillLevels.Include(a=>a.Employee).ThenInclude(a=>a.EmployeeSkillLevel).Include(a=>a.Skill).ThenInclude(a=>a.EmployeeSkillLevel).Include(a=>a.Level).ToListAsync().Result.FindAll(x => x.EmployeeId == userId);
            return users;
        }

        public void Update(IEnumerable<EmployeeSkillLevel> entities)
        {
            context.EmployeeSkillLevels.UpdateRange(entities);
        }
    }
}
