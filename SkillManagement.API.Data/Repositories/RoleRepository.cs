using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;

namespace SkillManagement.API.Data.Repositories
{
    public class RoleRepository:GenericRepository<Roles>, IRoleRepository
    {
        private SkillContext context;
        public RoleRepository(SkillContext context)
          : base(context)
        {
            this.context = context;

        }

        public IList<Roles> GetRoles(long[] roleIds)
        {
            return this.context.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
        }

     
    }
}
