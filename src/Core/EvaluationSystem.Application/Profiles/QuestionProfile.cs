using AutoMapper;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionTemplate, QuestionDto>();
            CreateMap<CreateQuestionDto, QuestionTemplate>();
            CreateMap<CreateQuestionDto, QuestionDto>();
            CreateMap<UpdateQuestionDto, QuestionTemplate>();
            CreateMap<GetQuestionsDto, QuestionDto>();
            CreateMap<QuestionTemplate, GetQuestionsDto>();
        }
    }
}
