using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Profiles
{
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            CreateMap<UpdateFormDto, FormTemplate>();
            CreateMap<FormTemplate, ExposeFormDto>();
            CreateMap<CreateGetFormDto, FormTemplate>();
        }
    }
}
