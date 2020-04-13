using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;

namespace SkillManagement.API.Core.Services
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Skill> SkillRepository { get; }

        IGenericRepository<Level> LevelRepository { get; }
                
        IUserSkillLevelRepository UserSkillLevelRepository { get; }
        void Save();
    }
}
