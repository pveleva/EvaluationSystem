using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IAttestationForm
{
    public interface IAttestationFormService
    {
        List<CreateGetFormDto> GetAll();
        CreateGetFormDto GetById(int id);
        CreateGetFormDto Create(CreateGetFormDto form);
    }
}
