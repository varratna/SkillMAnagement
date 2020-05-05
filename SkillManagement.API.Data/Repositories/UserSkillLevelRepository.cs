using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;

namespace SkillManagement.API.Data.Repositories
{
    public class UserSkillLevelRepository : GenericRepository<UserSkillLevel>, IUserSkillLevelRepository
    {
        private SkillContext context;

        public UserSkillLevelRepository(SkillContext context)
          : base(context)
        {
            this.context = context;

        }

        public new IEnumerable<UserSkillLevel> GetAll()
        {
            return context.UserSkillLevels.Include(a => a.User).ThenInclude(a => a.UserSkillLevel).Include(a => a.Skill).ThenInclude(a => a.UserSkillLevel).Include(a => a.Level).ToListAsync().Result;
        }

        public void DeleteByUserId(long userId)
        {
            var entitiesToDelete = context.UserSkillLevels.ToListAsync().Result.FindAll(x => x.UserId == userId);
            context.UserSkillLevels.RemoveRange(entitiesToDelete);
        }

        public IEnumerable<UserSkillLevel> GetByUserId(long userId)
        {

            var users = context.UserSkillLevels.Include(a=>a.User).ThenInclude(a=>a.UserSkillLevel).Include(a=>a.Skill).ThenInclude(a=>a.UserSkillLevel).Include(a=>a.Level).ToListAsync().Result.FindAll(x => x.UserId == userId);
            return users;
        }

        public void Update(IEnumerable<UserSkillLevel> entities)
        {
            context.UserSkillLevels.UpdateRange(entities);
        }
    }
}
