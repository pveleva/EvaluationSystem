using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationSystem.Persistence.EF.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<QuestionTemplate> builder)
        {
            builder.ToTable("QuestionTemplate");

            builder
                .HasMany<ModuleQuestion>()
                .WithOne()
                .HasForeignKey(mq => mq.IdQuestion);

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            //builder.Property(e => e.AnswerText)
            //    .HasMaxLength(200)
            //    .IsRequired();
        }
    }
}
