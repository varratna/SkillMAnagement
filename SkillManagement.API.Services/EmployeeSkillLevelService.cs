using System.Collections.Generic;
using System.Linq;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Services;

namespace SkillManagement.API.Services
{
    public class EmployeeSkillLevelService : IEmployeeSkillLevelService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeSkillLevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<EmployeeSkillLevel> GetAll()
        {
            var employeeSkillLevels = _unitOfWork.EmployeeSkillLevelRepository.GetAll();
            return employeeSkillLevels;
        }
        public IEnumerable<EmployeeSkillLevel> Get(long id)
        {
            var employeeSkillLevels = _unitOfWork.EmployeeSkillLevelRepository.GetAll().Where(m => m.EmployeeId == id);
            return employeeSkillLevels;
        }
        public IEnumerable<EmployeeSkillLevel> GetByEmployeeId(long employeeId)
        {
            var employeeSkillLevels = _unitOfWork.EmployeeSkillLevelRepository.GetByEmployeeId(employeeId);
            return employeeSkillLevels;
        }

        public void Add(EmployeeSkillLevel entity)
        {
            _unitOfWork.EmployeeSkillLevelRepository.Add(entity);
            _unitOfWork.Save();
        }


        public void Update(IEnumerable<EmployeeSkillLevel> employeeSkillLevels)
        {
            _unitOfWork.EmployeeSkillLevelRepository.Update(employeeSkillLevels);
            _unitOfWork.Save();

        }

        public void Update(EmployeeSkillLevel entity)
        {
            var employeeSkillLevel = _unitOfWork.EmployeeSkillLevelRepository.GetAll().Where(x => x.EmployeeId == entity.EmployeeId && x.SkillId == entity.SkillId).FirstOrDefault();
            entity.Id = employeeSkillLevel.Id;
            employeeSkillLevel.LevelId = entity.LevelId;

            _unitOfWork.EmployeeSkillLevelRepository.Update(employeeSkillLevel);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            _unitOfWork.EmployeeSkillLevelRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void DeleteByEmployeeId(long id)
        {
            _unitOfWork.EmployeeSkillLevelRepository.DeleteByEmployeeId(id);
            _unitOfWork.Save();
        }


    }
}
