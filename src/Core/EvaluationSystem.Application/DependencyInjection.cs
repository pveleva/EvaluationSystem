using EvaluationSystem.Application.Profiles;
using Microsoft.Extensions.DependencyInjection;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Services.Dapper;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IFormModule;
using EvaluationSystem.Application.Interfaces.IAttestation;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IModuleQuestionService, ModuleQuestionService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IFormModuleService, FormModuleService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IUser, CurrentUser>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAttestationService, AttestationService>();

            services.AddAutoMapper(typeof(AnswerProfile).Assembly);

            return services;
        }
    }
}
