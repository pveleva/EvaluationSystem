using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151623)]
    public class DropAttestationAnswerTable : Migration
    {
        public override void Up()
        {
            Delete.Table("AttestationAnswer");
        }
        public override void Down()
        {
            Create.Table("AttestationAnswer")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("IdAttestation").AsInt64().ForeignKey("Attestation", "Id").NotNullable()
                .WithColumn("IdUserParticipant").AsInt64().ForeignKey("User", "Id").NotNullable()
                .WithColumn("IdModuleTemplate").AsInt64().ForeignKey("ModuleTemplate", "Id").NotNullable()
                .WithColumn("IdQuestionTemplate").AsInt64().ForeignKey("QuestionTemplate", "Id").NotNullable()
                .WithColumn("IdAnswerTemplate").AsInt64().ForeignKey("AnswerTemplate", "Id").NotNullable()
                .WithColumn("TextAnswer").AsString(255).NotNullable();
        }
    }
}
