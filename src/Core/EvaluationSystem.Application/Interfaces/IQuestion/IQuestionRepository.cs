using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces.IQuestion
{
    public interface IQuestionRepository : IGenericRepository<QuestionTemplate>
    {
        public List<GetQuestionsDto> GetAll(int moduleId);
        List<GetQuestionsDto> GetAll();
        public List<GetQuestionsDto> GetByIDFromRepo(int moduleId, int questionId);
        List<GetQuestionsDto> GetByIDFromRepo(int questionId);
        public void DeleteFromRepo(int id);
    }
}
