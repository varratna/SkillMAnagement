using System.Collections.Generic;
using System.Linq;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;

namespace SkillManagement.API.Services
{
    public class UserSkillLevelService : IUserSkillLevelService
    {
        private IUnitOfWork _unitOfWork;

        public UserSkillLevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<UserSkillLevel> GetAll()
        {
            var userSkillLevels = _unitOfWork.UserSkillLevelRepository.GetAll();
            return userSkillLevels;
        }
        public IEnumerable<UserSkillLevel> Get(long id)
        {
            var userSkillLevels = _unitOfWork.UserSkillLevelRepository.GetAll().Where(m => m.UserId == id);
            return userSkillLevels;
        }
        public IEnumerable<UserSkillLevel> GetByUserId(long userId)
        {
            var userSkillLevels = _unitOfWork.UserSkillLevelRepository.GetByUserId(userId);
            return userSkillLevels;
        }

        public void Add(UserSkillLevel entity)
        {
            _unitOfWork.UserSkillLevelRepository.Add(entity);
            _unitOfWork.Save();
        }




        public void Update(UserSkillLevel entity)
        {
            var userSkillLevel = _unitOfWork.UserSkillLevelRepository.GetAll().Where(x => x.UserId == entity.UserId && x.SkillId == entity.SkillId).FirstOrDefault();
            entity.Id = userSkillLevel.Id;
            userSkillLevel.LevelId = entity.LevelId;

            _unitOfWork.UserSkillLevelRepository.Update(userSkillLevel);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            _unitOfWork.UserSkillLevelRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void DeleteByUserId(long id)
        {
            _unitOfWork.UserSkillLevelRepository.DeleteByUserId(id);
            _unitOfWork.Save();
        }


    }
}
