using System.Collections.Generic;

namespace SkillManagement.API.Core.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(long id);

        


    }
}
