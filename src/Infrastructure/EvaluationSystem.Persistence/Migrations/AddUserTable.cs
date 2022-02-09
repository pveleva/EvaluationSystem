using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112021611)]
    public class AddUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Name").AsString(255).NotNullable();
        }
        public override void Down()
        {
            Delete.Table("User");
        }
    }
}
