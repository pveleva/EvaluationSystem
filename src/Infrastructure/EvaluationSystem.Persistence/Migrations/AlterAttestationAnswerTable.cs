using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112140841)]
    public class AlterAttestationAnswerTable : Migration
    {
        public override void Up()
        {
            Alter.Table("AttestationAnswer")
                .AlterColumn("IdAnswerTemplate")
                .AsInt64().Nullable();
        }
        public override void Down()
        {
            Alter.Table("AttestationAnswer")
                .AlterColumn("IdAnswerTemplate")
                .AsInt64().NotNullable();
        }
    }
}
