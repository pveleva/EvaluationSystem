using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Profiles
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<CreateUpdateModuleDto, ModuleTemplate>();
            CreateMap<ModuleTemplate, ExposeModuleDto>();
        }
    }
}
