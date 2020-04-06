using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillManagement.API.Models;

namespace SkillManagement.API.Services
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
