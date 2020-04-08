using System.Collections.Generic;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Services
{
    public interface ISkillService
    {
        IEnumerable<Skill> GetAll();
        Skill Get(long id);
        void Update(Skill entity);
        void Add(Skill entity);
        void Delete(long id);
    }
}
