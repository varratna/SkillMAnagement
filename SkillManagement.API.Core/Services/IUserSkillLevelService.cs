using System.Collections.Generic;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Services
{
    public interface IUserSkillLevelService
    {
        IEnumerable<UserSkillLevel> GetAll();

        IEnumerable<UserSkillLevel> Get(long id);
        void Update(UserSkillLevel entity);

        void Update(IEnumerable<UserSkillLevel> entities);
        void Add(UserSkillLevel entity);
        void Delete(long id);

        void DeleteByUserId(long userId);

        IEnumerable<UserSkillLevel> GetByUserId(long userId);
    }
}
