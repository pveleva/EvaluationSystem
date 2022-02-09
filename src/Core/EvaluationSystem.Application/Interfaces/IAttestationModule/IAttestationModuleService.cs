using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Interfaces.IAttestationModule
{
    public interface IAttestationModuleService
    {
        List<GetModulesDto> GetAll();
        GetModulesDto GetById(int id);
        void Create(int formId, GetModulesDto form);
        public void DeleteFromRepo(int id);
    }
}
