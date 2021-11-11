using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Profiles;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Application.Services.Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddAutoMapper(typeof(AnswerProfile).Assembly);

            return services;
        }
    }
}
