using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetList();
        T GetByID(int id);
        int Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
