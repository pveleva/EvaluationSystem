using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Questions.Dapper
{
    public interface IQuestionRepository : IGenericRepository<QuestionTemplate>
    {
        public List<GetQuestionsDto> GetAll();
        public List<GetQuestionsDto> GetByIDFromRepo(int questionId);
        public void DeleteFromRepo(int id);
    }
}
