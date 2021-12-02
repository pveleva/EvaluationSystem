using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationSystem.Persistence.EF.Configurations
{
    public class ModuleQuestionConfiguration : IEntityTypeConfiguration<ModuleQuestion>
    {
        public void Configure(EntityTypeBuilder<ModuleQuestion> builder)
        {
        }
    }
}
