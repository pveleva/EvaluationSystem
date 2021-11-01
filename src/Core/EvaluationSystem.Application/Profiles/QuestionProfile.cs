using AutoMapper;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>();
        }
    }
}
