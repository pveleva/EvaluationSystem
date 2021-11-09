using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers.Dapper
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAllAnswers(int questionId);
        Answer GetAnswerById(int questionId, int answerId);
        void AddAnswerToDatabase(Answer answer);
        Answer UpdateAnswer(Answer answer);
        void DeleteAnswer(int questionId, int answerId);
    }
}
