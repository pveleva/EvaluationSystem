using Dapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IQuestion;

namespace EvaluationSystem.Persistence.Dapper
{
    public class QuestionRepository : BaseRepository<QuestionTemplate>, IQuestionRepository
    {
        public QuestionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        public List<GetQuestionsDto> GetAll(int moduleId)
        {
            string query = @"SELECT m.Id AS IdModule, m.[Name] AS NameModule, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM ModuleTemplate AS m
                                            LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                            ORDER BY q.Id, a.Id
                                            WHERE q.IsReusable = 0";
            return Connection.Query<GetQuestionsDto>(query, null, Transaction).AsList();
        }

        public List<GetQuestionsDto> GetByIDFromRepo(int moduleId, int questionId)
        {
            string query = @"SELECT m.Id AS IdModule, m.[Name] AS NameModule, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM ModuleTemplate AS m
                                            LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                            WHERE m.Id =@IdModule AND q.Id = @IdQuestion AND q.IsReusable = 0";
            return Connection.Query<GetQuestionsDto>(query, new { IdModule = moduleId, IdQuestion = questionId }, Transaction).AsList();
        }

        public List<GetQuestionsDto> GetAll()
        {
            string query = @"SELECT q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], 
	                                a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM QuestionTemplate AS q
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                            WHERE q.IsReusable = 1";
            return Connection.Query<GetQuestionsDto>(query, null, Transaction).AsList();
        }

        public List<GetQuestionsDto> GetByIDFromRepo(int questionId)
        {
            string query = @"SELECT q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], 
	                                a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM QuestionTemplate AS q
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                            WHERE q.Id = @Id AND q.IsReusable = 1";
            return Connection.Query<GetQuestionsDto>(query, new { Id = questionId }, Transaction).AsList();
        }

        public void DeleteFromRepo(int id)
        {
            string deleteAnswers = @"DELETE FROM AnswerTemplate WHERE IdQuestion = @Id";
            Connection.Execute(deleteAnswers, new { Id = id }, Transaction);

            string deleteMQ = @"DELETE FROM ModuleQuestion WHERE IdQuestion = @Id";
            Connection.Execute(deleteMQ, new { Id = id }, Transaction);

            Connection.Delete<QuestionTemplate>(id, Transaction);
        }
    }
}
