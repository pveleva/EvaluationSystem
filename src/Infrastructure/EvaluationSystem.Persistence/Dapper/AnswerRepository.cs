using Dapper;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AnswerRepository : DbConfigConnection, IAnswerRepository
    {
        public AnswerRepository(IConfiguration configuration)
            : base(configuration) { }

        public void AddAnswerToDatabase(Answer answer)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"INSERT INTO AnswerTemplate (IsDefault, Position, AnswerText, IdQuestion) VALUES (@IsDefault, @Position, @AnswerText, @IdQuestion)";
                dbConnection.Execute(query, answer);
            }
        }

        public void DeleteAnswer(int questionId, int answerId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"DELETE FROM AnswerTemplate WHERE IdQuestion = @IdQuestion AND Id = @Id";
                dbConnection.Execute(query, new { IdQuestion = questionId, Id = answerId });
            }
        }

        public IEnumerable<Answer> GetAllAnswers(int questionId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT * FROM AnswerTemplate WHERE IdQuestion = @IdQuestion";
                return dbConnection.Query<Answer>(query, new { IdQuestion = questionId });
            }
        }

        public Answer GetAnswerById(int questionId, int answerId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT * FROM AnswerTemplate WHERE IdQuestion = @IdQuestion AND Id = @Id";
                return dbConnection.QueryFirstOrDefault<Answer>(query, new { IdQuestion = questionId, Id = answerId });
            }
        }

        public Answer UpdateAnswer(Answer answer)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string firstQuery = @"UPDATE AnswerTemplate SET IsDefault = @IsDefault, Position = @Position, AnswerText = @AnswerText, IdQuestion = @IdQuestion 
                                      WHERE IdQuestion = @IdQuestion AND Id = @Id";
                dbConnection.Execute(firstQuery, answer);

                string secondQuery = @"SELECT * FROM AnswerTemplate WHERE IdQuestion = @IdQuestion AND Id = @Id";
                return dbConnection.QueryFirstOrDefault<Answer>(secondQuery, new { IdQuestion = answer.IdQuestion, Id = answer.Id });
            }
        }
    }
}
