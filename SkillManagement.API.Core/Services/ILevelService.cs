using System.Collections.Generic;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Services
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
