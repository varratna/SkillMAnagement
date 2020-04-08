using System.Collections.Generic;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;


namespace SkillManagement.API.Services
{
    public class LevelService : ILevelService
    {
        private IUnitOfWork _unitOfWork;

        public LevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public void Add(Level entity)
        {
            _unitOfWork.LevelRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            _unitOfWork.LevelRepository.Delete(id);
            _unitOfWork.Save();
        }

        public Level Get(long id)
        {
            var level = _unitOfWork.LevelRepository.Get(id);
            return level;
        }

        public IEnumerable<Level> GetAll()
        {
            var levels = _unitOfWork.LevelRepository.GetAll();
            return levels;
        }

        public void Update(Level entity)
        {
            _unitOfWork.LevelRepository.Update(entity);
            _unitOfWork.Save();
        }
    }
}
