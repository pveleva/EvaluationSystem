using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Questions.Dapper
{
    public interface IQuestionRepository
    {
        List<Question> GetAllQuestions();
        List<Question> GetQuestionById(int questionId);
        int AddQuestionToDatabase(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(int id);
    }
}
