using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Interfaces.IModule
{
    public interface IModuleService
    {
        List<CreateUpdateModuleDto> GetAll();
        CreateUpdateModuleDto GetById(int id);
        ExposeModuleDto Create(CreateUpdateModuleDto form);
        ExposeModuleDto Update(int id, CreateUpdateModuleDto formDto);
        void DeleteFromRepo(int id);
    }
}
