using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111191339)]
    public class AddFormTable : Migration
    {
        public override void Up()
        {
            Create.Table("FormTemplate")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("[Name]").AsString(1024).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("FormTemplate");
        }
    }
}
