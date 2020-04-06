using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillManagement.API.Models;

namespace SkillManagement.API.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Get(long id);
        void Update(User entity);
        void Add(User entity);
        void Delete(long id);
        
    }
}
