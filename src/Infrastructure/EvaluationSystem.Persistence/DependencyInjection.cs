using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Persistence.Dapper;
using EvaluationSystem.Persistence.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
