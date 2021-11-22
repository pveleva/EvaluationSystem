using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces.IForm
{
    public interface IFormRepository : IGenericRepository<FormTemplate>
    {
        public List<CreateUpdateFormDto> GetAll();
        public List<CreateUpdateFormDto> GetByIDFromRepo(int id);
        public void DeleteFromRepo(int id);
    }
}
