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
            CreateMap<Answer, AnswerDto>();
            CreateMap<CreateUpdateAnswerDto, Answer>();
            CreateMap<CreateUpdateAnswerDto, AnswerDto>();
            CreateMap<GetQuestionsDto, AnswerDto>();
        }
    }
}
