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
            CreateMap<CreateModuleQuestionDto, QuestionTemplate>();
            CreateMap<CreateModuleQuestionDto, QuestionDto>();
            CreateMap<UpdateQuestionDto, QuestionTemplate>();
            CreateMap<GetQuestionsDto, QuestionDto>();
            CreateMap<QuestionTemplate, GetQuestionsDto>();
            CreateMap<QuestionDto, QuestionTemplate>();
            CreateMap<QuestionDto, QuestionTemplate>();
            CreateMap<QuestionDto, QuestionDto>();
        }
    }
}
