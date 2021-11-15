using Dapper;
using EvaluationSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EvaluationSystem.Persistence
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        private IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("EvaluationSystemDBConnection"));

        public IEnumerable<T> GetList()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.GetList<T>().ToList();
            }
        }

        public T GetByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Get<T>(id);
            }
        }
        public int Create(T entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return (int)dbConnection.Insert(entity);
            }
        }

        public void Update(T entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Update(entity);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Delete(dbConnection.Get<T>(id));
            }
        }
    }
}
