using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkillManagement.API.Models.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(long id);

        void Delete(IEnumerable<TEntity> entities);


    }
}
