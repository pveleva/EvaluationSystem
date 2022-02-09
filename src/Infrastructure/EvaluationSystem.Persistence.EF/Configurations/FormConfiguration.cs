using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationSystem.Persistence.EF.Configurations
{
    public class FormConfiguration : IEntityTypeConfiguration<FormTemplate>
    {
        public void Configure(EntityTypeBuilder<FormTemplate> builder)
        {
            builder.ToTable("FormTemplate");

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
