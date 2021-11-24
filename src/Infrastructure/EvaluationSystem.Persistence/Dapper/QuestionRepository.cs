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
        public List<GetQuestionsDto> GetAll()
        {
            string query = @"SELECT q.Id AS IdQuestion, q.[Name], a.Id AS IdAnswer, a.AnswerText FROM AnswerTemplate AS a
                                     RIGHT JOIN QuestionTemplate AS q ON q.Id = a.IdQuestion
                                     ORDER BY q.Id, a.Id";
            return Connection.Query<GetQuestionsDto>(query, null, Transaction).AsList();
        }

        public List<GetQuestionsDto> GetByIDFromRepo(int questionId)
        {
            string query = @"SELECT q.Id AS IdQuestion, q.[Name], a.Id AS IdAnswer, a.AnswerText FROM AnswerTemplate AS a
                                 RIGHT JOIN QuestionTemplate AS q ON q.Id = a.IdQuestion
                                 WHERE q.Id = @Id";
            return Connection.Query<GetQuestionsDto>(query, new { Id = questionId }, Transaction).AsList();
        }

        public void DeleteFromRepo(int id)
        {
            string deleteAnswers = @"DELETE FROM AnswerTemplate WHERE IdQuestion = @Id";
            Connection.Execute(deleteAnswers, new { Id = id }, Transaction);

            Connection.Delete<QuestionTemplate>(id, Transaction);
        }
    }
}
