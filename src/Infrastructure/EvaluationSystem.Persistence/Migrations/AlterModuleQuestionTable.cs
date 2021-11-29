using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111261813)]
    public class AlterModuleQuestionTable : Migration
    {
        public override void Up()
        {
            Alter.Table("ModuleQuestion")
                .AddColumn("Position")
                .AsInt64();
        }

        public override void Down()
        {
            Delete.Column("Position").FromTable("ModuleQuestion");
        }
    }
}
