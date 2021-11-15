using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AnswerRepository : BaseRepository<AnswerTemplate>, IAnswerRepository
    {
        public AnswerRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public List<AnswerTemplate> GetAll(int questionId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return GetList().Where(a => a.IdQuestion == questionId).ToList();
            }
        }
    }
}
