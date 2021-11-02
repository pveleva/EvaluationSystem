using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers
{
    public interface IAnswerRepository
    {
        public List<Answer> GetAllAnswers(int questionId);
        Answer GetAnswerById(int questionId, int answerId);
        void AddAnswerToDatabase(Answer answer);
        Answer UpdateAnswer(Answer answer);
        void DeleteAnswer(int questionId, int answerId);
    }
}
