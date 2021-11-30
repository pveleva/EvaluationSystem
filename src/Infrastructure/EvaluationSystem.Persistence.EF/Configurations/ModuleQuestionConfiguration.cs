using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationSystem.Persistence.EF.Configurations
{
    public class ModuleQuestionConfiguration : IEntityTypeConfiguration<ModuleQuestion>
    {
        public void Configure(EntityTypeBuilder<ModuleQuestion> builder)
        {
            //builder.HasMany<QuestionTemplate>(s => s.IdQuestion)
            //    .WithMany(c => c.Students)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("StudentRefId");
            //        cs.MapRightKey("CourseRefId");
            //        cs.ToTable("StudentCourse");
            //    });
        }
    }
}
