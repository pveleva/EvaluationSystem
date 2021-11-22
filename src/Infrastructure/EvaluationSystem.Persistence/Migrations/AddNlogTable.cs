using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111181359)]
    public class AddNlogTable : Migration
    {
        public override void Up()
        {
            Create.Table("Nlogs")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("[Level]").AsInt32().NotNullable()
                .WithColumn("Message").AsString(int.MaxValue);
        }

        public override void Down()
        {
            Delete.Table("Nlogs");
        }
    }
}
