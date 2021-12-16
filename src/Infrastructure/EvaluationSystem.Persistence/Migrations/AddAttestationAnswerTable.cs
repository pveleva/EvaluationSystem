using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112061549)]
    public class AddAttestationAnswerTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationAnswer")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("IdAttestation").AsInt64().ForeignKey("Attestation", "Id").NotNullable()
                .WithColumn("IdUserParticipant").AsInt64().ForeignKey("User", "Id").NotNullable()
                .WithColumn("IdModuleTemplate").AsInt64().ForeignKey("ModuleTemplate", "Id").NotNullable()
                .WithColumn("IdQuestionTemplate").AsInt64().ForeignKey("QuestionTemplate", "Id").NotNullable()
                .WithColumn("IdAnswerTemplate").AsInt64().Nullable()
                .WithColumn("TextAnswer").AsString(255).Nullable();
        }
        public override void Down()
        {
            Delete.Table("AttestationAnswer");
        }
    }
}
