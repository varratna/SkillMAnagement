using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillManagement.API.Models;
using SkillManagement.API.Models.Repositories;

namespace SkillManagement.API.Services
{
    public class UserSkillLevelService : IUserSkillLevelService
    {
        private IUnitOfWork _unitOfWork;

        public UserSkillLevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void Add(UserSkillLevel entity)
        {
            _unitOfWork.UserSkillLevelRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void DeleteByUserId(long id)
        {
            var usersToDelete = _unitOfWork.UserSkillLevelRepository.GetAll().Where(x => x.UserId == id);

            _unitOfWork.UserSkillLevelRepository.Delete(usersToDelete);
            _unitOfWork.Save();
        }

        public IEnumerable<UserSkillLevel> GetAll()
        {
            var userSkillLevels = _unitOfWork.UserSkillLevelRepository.GetAll();
            return userSkillLevels;
        }

        public IEnumerable<UserSkillLevel> GetByUserId(long id)
        {
            var userSkillLevels = _unitOfWork.UserSkillLevelRepository.GetAll().Where(m => m.UserId == id);
            return userSkillLevels;
        }

        public void Update(UserSkillLevel entity)
        {
            _unitOfWork.UserSkillLevelRepository.Update(entity);
            _unitOfWork.Save();
        }
    }
}
