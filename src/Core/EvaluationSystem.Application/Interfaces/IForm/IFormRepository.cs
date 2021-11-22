using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IForm
{
    public interface IFormRepository : IGenericRepository<FormTemplate>
    {
        public List<GetFormModuleQuestionAnswerDto> GetAll();
        public List<GetFormModuleQuestionAnswerDto> GetByIDFromRepo(int id);
        public void DeleteFromRepo(int id);
    }
}
