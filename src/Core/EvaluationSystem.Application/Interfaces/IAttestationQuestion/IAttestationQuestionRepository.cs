using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Questions;

namespace EvaluationSystem.Application.Interfaces.IAttestationQuestion
{
    public interface IAttestationQuestionRepository : IGenericRepository<AttestationQuestion>
    {
        public List<GetQuestionsDto> GetAll(int moduleId);
        List<GetQuestionsDto> GetAll();
        public List<GetQuestionsDto> GetByIDFromRepo(int moduleId, int questionId);
        List<GetQuestionsDto> GetByIDFromRepo(int questionId);
        public void DeleteFromRepo(int id);
    }
}
