using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationSystem.Persistence.EF.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<AnswerTemplate>
    {
        public void Configure(EntityTypeBuilder<AnswerTemplate> builder)
        {
            builder.ToTable("AnswerTemplate");

            builder.Property(e => e.AnswerText)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(w => w.AnswerText)
                   .WithMany()
                   .HasForeignKey(w => w.IdQuestion)
                   .IsRequired();
        }
    }
}
