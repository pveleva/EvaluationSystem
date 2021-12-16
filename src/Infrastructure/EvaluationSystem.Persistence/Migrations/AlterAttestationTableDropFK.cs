using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151719)]
    public class AlterAttestationTableDropFK : Migration
    {
        public override void Up()
        {
            Execute.Sql("ALTER TABLE [Attestation] DROP CONSTRAINT FK_Attestation_IdForm_FormTemplate_Id;");
        }
        public override void Down()
        {
            Execute.Sql("ALTER TABLE [Attestation] ADD FOREIGN KEY (IdForm) REFERENCES FormTemplate(Id);");
        }
    }
}
