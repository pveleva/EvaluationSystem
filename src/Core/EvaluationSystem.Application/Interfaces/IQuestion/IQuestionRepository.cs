using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces.IQuestion
{
    public interface IQuestionRepository : IGenericRepository<QuestionTemplate>
    {
        public List<GetQuestionsDto> GetAll();
        public List<GetQuestionsDto> GetByIDFromRepo(int id);
        public void DeleteFromRepo(int id);
    }
}
