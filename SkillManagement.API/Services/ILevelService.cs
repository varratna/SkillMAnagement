using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillManagement.API.Models;

namespace SkillManagement.API.Services
{
    public interface ILevelService
    {
        IEnumerable<Level> GetAll();
        Level Get(long id);
        void Update(Level entity);
        void Add(Level entity);
        void Delete(long id);
    }
}
