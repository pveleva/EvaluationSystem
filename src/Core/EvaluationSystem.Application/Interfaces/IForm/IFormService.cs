using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IForm
{
    public interface IFormService
    {
        List<CreateGetFormDto> GetAll();
        CreateGetFormDto GetById(int id);
        CreateGetFormDto Create(CreateGetFormDto form);
        ExposeFormDto Update(int id, UpdateFormDto formDto);
        void DeleteFromRepo(int id);
    }
}
