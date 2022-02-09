using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Persistence.EF
{
    public class EvaluationSystemDbContext : DbContext
    {
        public EvaluationSystemDbContext(DbContextOptions<EvaluationSystemDbContext> options)
                : base(options)
        { }

        public DbSet<AnswerTemplate> AnswerTemplates { get; set; }
        public DbSet<QuestionTemplate> QuestionTemplates { get; set; }
        public DbSet<ModuleTemplate> ModuleTemplates { get; set; }
        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<ModuleQuestion> ModuleQuestions { get; set; }
        public DbSet<FormModule> FormModules { get; set; }

        private static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { });//builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EvaluationSystemDbContext).Assembly);
        }
    }
}
