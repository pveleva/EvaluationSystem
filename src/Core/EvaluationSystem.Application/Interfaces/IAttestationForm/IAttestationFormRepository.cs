using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IAttestationForm
{
    public interface IAttestationFormRepository : IGenericRepository<AttestationForm>
    {
        public List<GetFormModuleQuestionAnswerDto> GetAll();
        public List<GetFormModuleQuestionAnswerDto> GetByIDFromRepo(int id);
    }
}

