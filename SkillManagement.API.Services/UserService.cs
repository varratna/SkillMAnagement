using System.Collections.Generic;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;

namespace SkillManagement.API.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void Add(User entity)
        {
            _unitOfWork.UserRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.Save();
        }

        public User Get(long id)
        {
            var user = _unitOfWork.UserRepository.Get(id);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return users;
        }

        public void Update(User entity)
        {
            _unitOfWork.UserRepository.Update(entity);
            _unitOfWork.Save();
        }
    }
}

