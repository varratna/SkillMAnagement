using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillManagement.API.Models;

namespace SkillManagement.API.Services
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
