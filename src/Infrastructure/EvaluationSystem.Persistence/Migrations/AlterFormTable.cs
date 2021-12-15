using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112150957)]
    public class AlterFormTable : Migration
    {
        public override void Up()
        {
            Alter.Table("FormTemplate")
                .AlterColumn("[Name]")
                .AsString(1024).Unique().NotNullable();
        }
        public override void Down()
        {
            Alter.Table("FormTemplate")
                .AlterColumn("[Name]")
                .AsString(1024).NotNullable();
        }
    }
}
