using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;

namespace EvaluationSystem.Persistence
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> Entity;

        public BaseRepository(DbContext dbContext)
        {
            Entity = dbContext.Set<T>();
        }
        IEnumerable<T> IGenericRepository<T>.GetList()
        {
            throw new System.NotImplementedException();
        }

        public List<T> GetList()
        {
            return Entity.ToList();
        }

        public T GetByID(int id)
        {
            return Entity.Find(id);
        }

        public int Create(T entity)
        {
            return 1; // Entity.Add(entity);
        }

        public void Update(T entity)
        {
            Entity.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            Entity.Remove(entity);
        }

    }
}
