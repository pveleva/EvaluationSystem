using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AnswerTemplate, AnswerDto>();
            CreateMap<AnswerDto, AnswerTemplate>();
            CreateMap<CreateUpdateAnswerDto, AnswerTemplate>();
            CreateMap<CreateUpdateAnswerDto, AnswerDto>();
            CreateMap<GetQuestionsDto, AnswerDto>();
        }
    }
}
