using EvaluationSystem.Application.Questions;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces.IQuestion
{
    public interface IQuestionService
    {
        List<QuestionDto> GetAll();
        QuestionDto GetById(int id);
        QuestionDto Create(CreateQuestionDto question);
        QuestionDto Update(int id, UpdateQuestionDto questionDto);
        void Delete(int id);
    }
}
