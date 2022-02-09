using Dapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestationQuestion;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationQuestionRepository : BaseRepository<AttestationQuestion>, IAttestationQuestionRepository
    {
        public AttestationQuestionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        public List<GetQuestionsDto> GetAll(int moduleId)
        {
            string query = @"  SELECT m.Id AS IdModule, m.[Name] AS NameModule, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], q.DateOfCreation, a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM AttestationModule AS m
                                            LEFT JOIN AttestationModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN AttestationQuestion AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AttestationAnswer AS a ON a.IdQuestion = q.Id
                                            WHERE mq.IdModule =@IdModule AND q.IsReusable = 0";
            return Connection.Query<GetQuestionsDto>(query, new { IdModule = moduleId }, Transaction).AsList();
        }

        public List<GetQuestionsDto> GetByIDFromRepo(int moduleId, int questionId)
        {
            string query = @"SELECT m.Id AS IdModule, m.[Name] AS NameModule, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], q.DateOfCreation, a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM AttestationModule AS m
                                            LEFT JOIN AttestationModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN AttestationQuestion AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AttestationAnswer AS a ON a.IdQuestion = q.Id
                                            WHERE m.Id =@IdModule AND q.Id = @IdQuestion AND q.IsReusable = 0";
            return Connection.Query<GetQuestionsDto>(query, new { IdModule = moduleId, IdQuestion = questionId }, Transaction).AsList();
        }

        public List<GetQuestionsDto> GetAll()
        {
            string query = @"SELECT q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], q.DateOfCreation, 
	                                a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM AttestationQuestion AS q
                                            LEFT JOIN AttestationAnswer AS a ON a.IdQuestion = q.Id
                                            WHERE q.IsReusable = 1";
            return Connection.Query<GetQuestionsDto>(query, null, Transaction).AsList();
        }

        public List<GetQuestionsDto> GetByIDFromRepo(int questionId)
        {
            string query = @"SELECT q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], q.DateOfCreation,
	                                a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM AttestationQuestion AS q
                                            LEFT JOIN AttestationAnswer AS a ON a.IdQuestion = q.Id
                                            WHERE q.Id = @Id AND q.IsReusable = 1";
            return Connection.Query<GetQuestionsDto>(query, new { Id = questionId }, Transaction).AsList();
        }
        public void DeleteFromRepo(int id)
        {
            string deleteMQ = @"DELETE FROM AttestationModuleQuestion WHERE IdQuestion = @Id";
            Connection.Execute(deleteMQ, new { Id = id }, Transaction);

            string deleteAnswers = @"DELETE FROM AttestationAnswer WHERE IdQuestion = @Id";
            Connection.Execute(deleteAnswers, new { Id = id }, Transaction);

            Connection.Delete<AttestationQuestion>(id, Transaction);
        }
    }
}
