using Dapper;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EvaluationSystem.Persistence.Dapper
{
    public class QuestionRepository : IQuestionRepository
    {
        protected readonly IConfiguration _configuration;
        public QuestionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("EvaluationSystemDBConnection"));
        public List<GetQuestionsDto> GetAllQuestions()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT q.Id AS IdQuestion, q.[Name], a.Id AS IdAnswer, a.AnswerText FROM AnswerTemplate AS a
                                     RIGHT JOIN QuestionTemplate AS q ON q.Id = a.IdQuestion
                                     ORDER BY q.Id, a.Id";
                return dbConnection.Query<GetQuestionsDto>(query).AsList();
            }
        }

        public List<GetQuestionsDto> GetQuestionById(int questionId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT q.Id AS IdQuestion, q.[Name], a.Id AS IdAnswer, a.AnswerText FROM AnswerTemplate AS a
                                 RIGHT JOIN QuestionTemplate AS q ON q.Id = a.IdQuestion
                                 WHERE q.Id = @Id";
                return dbConnection.Query<GetQuestionsDto>(query, new { Id = questionId }).AsList();
            }
        }

        public int AddQuestionToDatabase(Question question)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"INSERT INTO QuestionTemplate ([Name], [Type], IsReusable) OUTPUT inserted.Id VALUES (@Name, @Type, @IsReusable)";
                var index = dbConnection.QuerySingle<int>(query, question);
                return index;
            }
        }

        public void UpdateQuestion(Question question)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"UPDATE QuestionTemplate SET [Name] = @Name, [Type] = @Type, IsReusable = @IsReusable
                                      WHERE Id = @Id";
                dbConnection.Execute(query, question);
            }
        }

        public void DeleteQuestion(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string deleteAnswers = @"DELETE FROM AnswerTemplate WHERE IdQuestion = @Id";
                dbConnection.Execute(deleteAnswers, new { Id = id });

                string deleteQuestion = @"DELETE FROM QuestionTemplate WHERE Id = @Id";
                dbConnection.Execute(deleteQuestion, new { Id = id });
            }
        }
    }
}
