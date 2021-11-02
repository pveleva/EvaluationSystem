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
            CreateMap<CreateAnswerDto, Answer>();
            CreateMap<CreateAnswerDto, AnswerDto>();
            CreateMap<UpdateAnswerDto, Answer>();
            CreateMap<UpdateAnswerDto, AnswerDto>();
        }
    }
}
