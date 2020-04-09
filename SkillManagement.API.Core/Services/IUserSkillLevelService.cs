using System.Collections.Generic;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Services
{
    public interface IUserSkillLevelService
    {
        IEnumerable<UserSkillLevel> GetAll();


        IEnumerable<UserSkillLevel> GetByUserId(long id);
        void Update(UserSkillLevel entity);
        void Add(UserSkillLevel entity);
        void DeleteByUserId(long id);        
    }
}
