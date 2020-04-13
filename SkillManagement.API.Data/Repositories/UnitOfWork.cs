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
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Skill> _skillRepository;
        private IGenericRepository<Level> _levelRepository;
        //private IGenericRepository<UserSkillLevel> _userSkillLevelRepository;
        private IUserSkillLevelRepository _userSkillLevelRepository;
        public UnitOfWork(SkillContext context)
        {
            _context = context;
        }

        public IGenericRepository<User> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new GenericRepository<User>(_context)); }
        }

        public IGenericRepository<Skill> SkillRepository
        {
            get { return _skillRepository ?? (_skillRepository = new GenericRepository<Skill>(_context)); }
        }

        public IGenericRepository<Level> LevelRepository
        {
            get { return _levelRepository ?? (_levelRepository = new GenericRepository<Level>(_context)); }
        }

        public IUserSkillLevelRepository UserSkillLevelRepository
        {
            get { return _userSkillLevelRepository ?? (_userSkillLevelRepository = new UserSkillLevelRepository(_context)); }
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
