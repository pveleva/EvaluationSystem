using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Questions
{
    public interface IQuestionRepository
    {
        Question GetQuestionById(int questionId);
        Answer GetQuestionAnswer(int questionId, int answerId);
        void AddQuestionToDatabase(Question question);
        bool UpdateQuestion(Question question);
        bool DeleteQuestion(int id);
        string DeleteQuestionAnswer(int questionId, int answerId);
    }
}
