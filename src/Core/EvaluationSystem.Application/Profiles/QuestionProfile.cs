using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Questions;

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
            CreateMap<UpdateCustomQuestionDto, QuestionTemplate>();
            CreateMap<GetQuestionsDto, QuestionDto>();
            CreateMap<QuestionTemplate, GetQuestionsDto>();
            CreateMap<QuestionDto, QuestionTemplate>();
            CreateMap<QuestionDto, QuestionTemplate>();

            CreateMap<AttestationQuestion, QuestionDto>();
            CreateMap<CreateModuleQuestionDto, AttestationQuestion>();
            CreateMap<UpdateQuestionDto, AttestationQuestion>();
            CreateMap<UpdateCustomQuestionDto, AttestationQuestion>();
            CreateMap<AttestationQuestion, GetQuestionsDto>();
            CreateMap<QuestionDto, AttestationQuestion>();
            CreateMap<QuestionDto, AttestationQuestion>();
        }
    }
}
