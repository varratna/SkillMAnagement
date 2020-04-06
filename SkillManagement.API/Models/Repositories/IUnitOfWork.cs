using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillManagement.API.Models.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Skill> SkillRepository { get; }

        IGenericRepository<Level> LevelRepository { get; }

        IGenericRepository<UserSkillLevel> UserSkillLevelRepository { get; }
        void Save();
    }
}
