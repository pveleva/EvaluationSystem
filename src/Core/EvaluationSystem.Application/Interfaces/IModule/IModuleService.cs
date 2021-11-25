using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Interfaces.IModule
{
    public interface IModuleService
    {
        List<GetModulesDto> GetAll();
        GetModulesDto GetById(int id);
        ExposeModuleDto Create(CreateModelDto form);
        ExposeModuleDto Update(int id, UpdateModuleDto formDto);
        void DeleteFromRepo(int id);
    }
}
