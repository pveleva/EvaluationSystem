using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers
{
    public interface IAnswerRepository
    {
        Answer GetAnswerById(int questionId, int answerId);
        void AddAnswerToDatabase(Answer answer);
        bool UpdateAnswer(Answer answer);
        bool DeleteAnswer(int id);
    }
}
