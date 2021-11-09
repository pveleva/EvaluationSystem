using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerDto>();
            CreateMap<CreateUpdateAnswerDto, Answer>();
            CreateMap<CreateUpdateAnswerDto, AnswerDto>();
        }
    }
}
