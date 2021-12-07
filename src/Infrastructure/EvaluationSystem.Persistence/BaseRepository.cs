using Dapper;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Application.Interfaces;

namespace EvaluationSystem.Persistence
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IDbConnection Connection => _unitOfWork.Connection;
        public IDbTransaction Transaction => _unitOfWork.Transaction;

        public IEnumerable<T> GetList()
        {
            return Connection.GetList<T>(null, null, Transaction).ToList();
        }

        public T GetByID(int id)
        {
            return Connection.Get<T>(id, Transaction);
        }
        public int Create(T entity)
        {
            return (int)Connection.Insert(entity, Transaction);
        }

        public void Update(T entity)
        {
            Connection.Update(entity, Transaction);
        }

        public void Delete(int id)
        {
            Connection.Delete(Connection.Get<T>(id, Transaction), Transaction);
        }
    }
}
