using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Questions
{
    public interface IQuestionRepository
    {
        List<Question> GetAllQuestions();
        Question GetQuestionById(int questionId);
        void AddQuestionToDatabase(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(int id);
    }
}
