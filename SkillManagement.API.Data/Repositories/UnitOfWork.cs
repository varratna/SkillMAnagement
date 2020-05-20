using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;
using SkillManagement.API.Core.Services;

namespace SkillManagement.API.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, System.IDisposable
    {
        private readonly SkillContext _context;
        private IEmployeeRepository _employeeeRepository;
        private IGenericRepository<Skill> _skillRepository;
        private IGenericRepository<Level> _levelRepository;
        //private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        //private IGenericRepository<UserSkillLevel> _userSkillLevelRepository;
        private IEmployeeSkillLevelRepository _employeeSkillLevelRepository;
        public UnitOfWork(SkillContext context)
        {
            _context = context;
        }

        public IRoleRepository RoleRepository
        {
            
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        }
        //public IUserRepository UserRepository
        //{
        //    //get { return _userRepository ?? (_userRepository = new GenericRepository<User>(_context)); }
        //    get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        //}
        public IEmployeeRepository EmployeeRepository
        {
            get { return _employeeeRepository ?? (_employeeeRepository = new EmployeeRepository(_context)); }
        }

        public IGenericRepository<Skill> SkillRepository
        {
            get { return _skillRepository ?? (_skillRepository = new GenericRepository<Skill>(_context)); }
        }

        public IGenericRepository<Level> LevelRepository
        {
            get { return _levelRepository ?? (_levelRepository = new GenericRepository<Level>(_context)); }
        }

        public IEmployeeSkillLevelRepository EmployeeSkillLevelRepository
        {
            get { return _employeeSkillLevelRepository ?? (_employeeSkillLevelRepository = new EmployeeSkillLevelRepository(_context)); }
        }



        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
