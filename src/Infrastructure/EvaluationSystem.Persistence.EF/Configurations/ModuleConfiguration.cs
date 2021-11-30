using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationSystem.Persistence.EF.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<ModuleTemplate>
    {
        public void Configure(EntityTypeBuilder<ModuleTemplate> builder)
        {
            builder.ToTable("ModuleTemplate");

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
