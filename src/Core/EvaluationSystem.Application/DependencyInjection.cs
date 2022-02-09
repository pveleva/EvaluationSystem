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
using EvaluationSystem.Application.Interfaces.IUserAnswer;
using EvaluationSystem.Application.Interfaces.IAttestation;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;
using EvaluationSystem.Application.Interfaces.IAttestationForm;
using EvaluationSystem.Application.Interfaces.IAttestationModule;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;
using EvaluationSystem.Application.Interfaces.IAttestationQuestion;
using EvaluationSystem.Application.Interfaces.IAttestationFormModule;
using EvaluationSystem.Application.Interfaces.IAttestationModuleQuestion;

namespace EvaluationSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IAttestationAnswerService, AttestationAnswerService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAttestationQuestionService, AttestationQuestionService>();
            services.AddScoped<IModuleQuestionService, ModuleQuestionService>();
            services.AddScoped<IAttestationModuleQuestionService, AttestationModuleQuestionService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IAttestationModuleService, AttestationModuleService>();
            services.AddScoped<IFormModuleService, FormModuleService>();
            services.AddScoped<IAttestationFormModuleService, AttestationFormModuleService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IAttestationFormService, AttestationFormService>();
            services.AddScoped<IUser, UserDto>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAttestationService, AttestationService>();
            services.AddScoped<IUserAnswerService, UserAnswerService>();

            services.AddAutoMapper(typeof(AnswerProfile).Assembly);

            return services;
        }
    }
}
