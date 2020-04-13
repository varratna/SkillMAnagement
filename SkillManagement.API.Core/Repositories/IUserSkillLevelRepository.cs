using System;
using System.Collections.Generic;
using System.Text;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Repositories
{
    public interface IUserSkillLevelRepository : IGenericRepository<UserSkillLevel>
    {
        IEnumerable<UserSkillLevel> GetByUserId(long userId);
        void DeleteByUserId(long userId);
    }
}
