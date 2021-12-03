using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112031049)]
    public class AlterNlogTable : Migration
    {
        public override void Up()
        {
            Alter.Table("Nlogs")
                .AlterColumn("Date")
                .AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
        }
        public override void Down()
        {
            Alter.Table("Nlogs")
                .AlterColumn("Date")
                .AsDateTime().NotNullable();
        }
    }
}
