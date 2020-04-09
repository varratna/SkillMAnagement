using System.Collections.Generic;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;

namespace SkillManagement.API.Services
{
    public class SkillService : ISkillService
    {
        private IUnitOfWork _unitOfWork;

        public SkillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void Add(Skill entity)
        {
            _unitOfWork.SkillRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            _unitOfWork.SkillRepository.Delete(id);
            _unitOfWork.Save();
        }

        public Skill Get(long id)
        {
            var skill = _unitOfWork.SkillRepository.Get(id);
            return skill;
        }

        public IEnumerable<Skill> GetAll()
        {
            var skills = _unitOfWork.SkillRepository.GetAll();
            return skills;
        }

        public void Update(Skill entity)
        {
            var skill = _unitOfWork.SkillRepository.Get(entity.Id);
            skill.SkillName= entity.SkillName;
            skill.Description = entity.Description;
            

            _unitOfWork.SkillRepository.Update(skill);
            _unitOfWork.Save();
        }
    }
}
