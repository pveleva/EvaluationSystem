using System.Collections.Generic;
using EvaluationSystem.Application.Questions;

namespace EvaluationSystem.Application.Interfaces.IAttestationQuestion
{
    public interface IAttestationQuestionService
    {
        List<QuestionDto> GetAll(int moduleId);
        List<QuestionDto> GetAll();
        QuestionDto GetById(int moduleId, int questionId);
        QuestionDto GetById(int id);
        QuestionDto Create(int moduleId, QuestionDto questionDto);
        QuestionDto Create(QuestionDto questionDto);
        public void Delete(int id);
    }
}

