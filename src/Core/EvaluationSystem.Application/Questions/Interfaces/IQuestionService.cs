using EvaluationSystem.Application.Answers;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Questions
{
    public interface IQuestionService
    {
        QuestionDto GetQuestionById(int id);
        AnswerDto GetQuestionAnswer(int questionId, int answerId);
        Question CreateQuestion(CreateQuestionDto question);
        string UpdateQuestion(UpdateQuestionDto questionDto);
        string DeleteQuestion(int id);
        string DeleteQuestionAnswer(int questionId, int answerId);
    }
}
