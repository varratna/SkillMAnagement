using System;
using System.Collections.Generic;
using System.Text;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Get(long id);
        void Update(User entity);
        void Add(User entity);
        void Delete(long id);

        string Authenticate(string username, string password);
    }
}
