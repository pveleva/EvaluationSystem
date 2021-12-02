using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationSystem.Persistence.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EvaluationSystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EvaluationSystemEFConnection"),
                    builder => builder.MigrationsAssembly(typeof(EvaluationSystemDbContext).GetTypeInfo().Assembly.GetName()
                        .Name)));

            services.AddScoped<DbContext, EvaluationSystemDbContext>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.Scan(scan => scan
            //    .FromAssembliesOf(typeof(BaseRepository<>))
            //    .AddClasses(f => f.AssignableTo(typeof(IGenericRepository<>)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime()
            //);

            return services;
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app, EvaluationSystemDbContext context)
        {
            context.Database.Migrate();
            return app;
        }
    }
}
