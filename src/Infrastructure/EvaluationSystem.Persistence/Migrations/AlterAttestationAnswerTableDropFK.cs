using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112141107)]
    public class AlterAttestationAnswerTableDropFK : Migration
    {
        public override void Up()
        {
            Execute.Sql("ALTER TABLE [AttestationAnswer] DROP CONSTRAINT FK_AttestationAnswer_IdAnswerTemplate_AnswerTemplate_Id;");
        }
        public override void Down()
        {
            Execute.Sql("ALTER TABLE [AttestationAnswer] ADD FOREIGN KEY (IdAnswerTemplate) REFERENCES AnswerTemplate(Id);");
        }
    }
}
