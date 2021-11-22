using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Profiles
{
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            CreateMap<CreateUpdateFormDto, FormTemplate>();
            CreateMap<FormTemplate, ExposeFormDto>();
        }
    }
}
