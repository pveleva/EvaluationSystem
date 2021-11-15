using Dapper;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace EvaluationSystem.Persistence.Dapper
{
    public class QuestionRepository : BaseRepository<QuestionTemplate>, IQuestionRepository
    {
        public QuestionRepository(IConfiguration configuration)
            : base(configuration)
        {
        }
        public List<GetQuestionsDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT q.Id AS IdQuestion, q.[Name], a.Id AS IdAnswer, a.AnswerText FROM AnswerTemplate AS a
                                     RIGHT JOIN QuestionTemplate AS q ON q.Id = a.IdQuestion
                                     ORDER BY q.Id, a.Id";
                return dbConnection.Query<GetQuestionsDto>(query).AsList();
            }
        }

        public List<GetQuestionsDto> GetByIDFromRepo(int questionId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT q.Id AS IdQuestion, q.[Name], a.Id AS IdAnswer, a.AnswerText FROM AnswerTemplate AS a
                                 RIGHT JOIN QuestionTemplate AS q ON q.Id = a.IdQuestion
                                 WHERE q.Id = @Id";
                return dbConnection.Query<GetQuestionsDto>(query, new { Id = questionId }).AsList();
            }
        }

        public void DeleteFromRepo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string deleteAnswers = @"DELETE FROM AnswerTemplate WHERE IdQuestion = @Id";
                dbConnection.Execute(deleteAnswers, new { Id = id });

                dbConnection.Delete<QuestionTemplate>(id);
            }
        }
    }
}
