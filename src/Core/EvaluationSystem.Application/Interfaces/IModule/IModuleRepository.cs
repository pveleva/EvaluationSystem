using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Interfaces.IModule
{
    public interface IModuleRepository : IGenericRepository<ModuleTemplate>
    {
        public List<GetModulesDto> GetAll();
        public List<GetModulesDto> GetByIDFromRepo(int id);
        public void DeleteFromRepo(int id);
    }
}
