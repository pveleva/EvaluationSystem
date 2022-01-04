using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202201040937)]
    public class AlterTablesToBeUnique : Migration
    {
        public override void Up()
        {
            Alter.Table("QuestionTemplate")
                .AlterColumn("[Name]").AsString(255).Unique().NotNullable();

            Alter.Table("ModuleTemplate")
                .AlterColumn("[Name]").AsString(255).Unique().NotNullable();
        }
        public override void Down()
        {
            Alter.Table("QuestionTemplate")
                .AlterColumn("[Name]").AsString(255).NotNullable();

            Alter.Table("ModuleTemplate")
                .AlterColumn("[Name]").AsString(255).NotNullable();
        }
    }
}
