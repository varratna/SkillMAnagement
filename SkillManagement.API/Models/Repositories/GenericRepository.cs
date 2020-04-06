using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SkillManagement.API.Models.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private SkillContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(SkillContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(long id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public TEntity Get(long id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(IEnumerable<TEntity> entities)
        {

            dbSet.RemoveRange(entities);



        }
    }
}
