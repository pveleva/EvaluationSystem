using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Persistence.Dapper;
using EvaluationSystem.Persistence.Migrations;
using EvaluationSystem.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IFormModule;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                    .ConfigureRunner(
                            builder => builder
                            .AddSqlServer()
                            .WithGlobalConnectionString(configuration.GetConnectionString("EvaluationSystemDBConnection"))
                            .ScanIn(typeof(AddQuestionTable).Assembly).For.Migrations())
                    .BuildServiceProvider();

            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IModuleQuestionRepository, ModuleQuestionRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IFormModuleRepository, FormModuleRepository>();
            services.AddScoped<IFormRepository, FormRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
