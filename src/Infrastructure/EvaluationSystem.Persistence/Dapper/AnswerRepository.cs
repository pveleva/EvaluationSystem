using Dapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Answers.Dapper;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AnswerRepository : BaseRepository<AnswerTemplate>, IAnswerRepository
    {
        public AnswerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<AnswerTemplate> GetAll(int questionId)
        {
            string query = @"SELECT * FROM AnswerTemplate WHERE IdQuestion = @questionId";
            var result = Connection.Query<AnswerTemplate>(query, new { questionId = questionId }, Transaction);
            return (List<AnswerTemplate>)result;
        }
    }
}
