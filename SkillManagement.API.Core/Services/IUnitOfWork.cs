using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;

namespace SkillManagement.API.Core.Services
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IGenericRepository<Skill> SkillRepository { get; }

        IGenericRepository<Level> LevelRepository { get; }
                
        IEmployeeSkillLevelRepository EmployeeSkillLevelRepository { get; }

        IRoleRepository RoleRepository { get; }

        //IUserRepository  UserRepository { get; }
        void Save();
    }
}
