using EvaluationSystem.Application.Questions;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces.IQuestion
{
    public interface IQuestionService
    {
        List<QuestionDto> GetAll(int moduleId);
        List<QuestionDto> GetAll();
        QuestionDto GetById(int moduleId, int questionId);
        QuestionDto GetById(int id);
        QuestionDto Create(int moduleId, QuestionDto questionDto);
        QuestionDto Create(QuestionDto questionDto);
        QuestionDto Update(int moduleId, int questionId, UpdateCustomQuestionDto questionDto);
        QuestionDto Update(int id, UpdateQuestionDto questionDto);
        void Delete(int id);
    }
}

