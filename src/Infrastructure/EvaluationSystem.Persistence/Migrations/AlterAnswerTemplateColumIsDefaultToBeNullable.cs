using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111291337)]
    class AlterAnswerTemplateColumIsDefaultToBeNullable : Migration
    {
        public override void Up()
        {
            Alter.Table("AnswerTemplate")
                .AlterColumn("IsDefault")
                .AsBoolean()
                .Nullable();
        }
        public override void Down()
        {
            Alter.Table("AnswerTemplate")
                .AlterColumn("IsDefault")
                .AsBoolean()
                .NotNullable();
        }
    }
}
