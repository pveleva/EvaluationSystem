using EvaluationSystem.Application;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Profiles;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Application.Services.Dapper;
using EvaluationSystem.Persistence;
using EvaluationSystem.Persistence.Dapper;
using EvaluationSystem.Persistence.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EvaluationSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvaluationSystem.API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();  //?
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EvaluationSystem.API v1"));
            }

            CreateDatabase
                .EnsureDatabase("Data Source=INTERN06-PC\\SQLEXPRESS;Initial Catalog=master; Integrated Security=True; MultipleActiveResultSets=True;", "EvaluationSystemDB");

            app.UpdateDatabase();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
