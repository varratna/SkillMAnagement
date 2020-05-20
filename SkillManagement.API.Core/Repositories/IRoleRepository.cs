using System;
using System.Collections.Generic;
using System.Text;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Core.Repositories
{
    public interface IRoleRepository: IGenericRepository<Roles>
    {
        IList<Roles> GetRoles(long[] roleIds);
    }
}
