using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IAttestationModule
{
    public interface IAttestationModuleRepository : IGenericRepository<AttestationModule>
    {
        public List<GetFormModuleQuestionAnswerDto> GetAll();
        public List<GetFormModuleQuestionAnswerDto> GetByIDFromRepo(int id);
        public void DeleteFromRepo(int id);
    }
}
