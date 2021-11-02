using EvaluationSystem.Application.Answers;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Questions
{
    public interface IQuestionService
    {
        List<QuestionDto> GetAllQuestions();
        QuestionDto GetQuestionById(int id);
        QuestionDto CreateQuestion(CreateQuestionDto question);
        QuestionDto UpdateQuestion(UpdateQuestionDto questionDto);
        void DeleteQuestion(int id);
    }
}
