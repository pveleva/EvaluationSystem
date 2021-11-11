using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Persistence.Dapper;
using EvaluationSystem.Persistence.Migrations;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
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
                            .ScanIn(typeof(CreateDatabase).Assembly).For.Migrations())
                    .Configure<SelectingProcessorAccessorOptions>(cfg => { cfg.ProcessorId = "sqlserver2008"; })
                    .BuildServiceProvider();

            services.AddFluentMigratorCore()
                    .ConfigureRunner(
                            builder => builder
                            .AddSqlServer()
                            .WithGlobalConnectionString(configuration.GetConnectionString("EvaluationSystemDBConnection"))
                            .ScanIn(typeof(AddQuestionTable).Assembly).For.Migrations())
                    .Configure<SelectingProcessorAccessorOptions>(cfg => {cfg.ProcessorId = "sqlserver2008";})
                    .BuildServiceProvider();

            services.AddFluentMigratorCore()
                    .ConfigureRunner(
                            builder => builder
                            .AddSqlServer()
                            .WithGlobalConnectionString(configuration.GetConnectionString("EvaluationSystemDBConnection"))
                            .ScanIn(typeof(AddAnswerTable).Assembly).For.Migrations())
                    .Configure<SelectingProcessorAccessorOptions>(cfg => { cfg.ProcessorId = "sqlserver";})
                    .BuildServiceProvider();

            services.AddFluentMigratorCore()
                    .ConfigureRunner(
                            builder => builder
                            .AddSqlServer()
                            .WithGlobalConnectionString(configuration.GetConnectionString("EvaluationSystemDBConnection"))
                            .ScanIn(typeof(CreateQuestionData).Assembly).For.Migrations())
                    .Configure<SelectingProcessorAccessorOptions>(cfg => { cfg.ProcessorId = "sqlserver"; })
                    .BuildServiceProvider();

            services.AddFluentMigratorCore()
                    .ConfigureRunner(
                            builder => builder
                            .AddSqlServer()
                            .WithGlobalConnectionString(configuration.GetConnectionString("EvaluationSystemDBConnection"))
                            .ScanIn(typeof(CreateAnswerData).Assembly).For.Migrations())
                    .Configure<SelectingProcessorAccessorOptions>(cfg => { cfg.ProcessorId = "sqlserver"; })
                    .BuildServiceProvider();

            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            return services;
        }

    }
}
