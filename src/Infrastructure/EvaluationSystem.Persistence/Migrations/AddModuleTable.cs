using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111191340)]
    public class AddModuleTable : Migration
    {
        public override void Up()
        {
            Create.Table("ModuleTemplate")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("[Name]").AsString(1024).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ModuleTemplate");
        }
    }
}
