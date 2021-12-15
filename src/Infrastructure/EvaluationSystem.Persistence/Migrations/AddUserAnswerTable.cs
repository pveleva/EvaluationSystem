using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151655)]
    public class AddUserAnswerTable : Migration
    {
        public override void Up()
        {
            Create.Table("UserAnswer")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("IdAttestation").AsInt64().ForeignKey("Attestation", "Id").NotNullable()
                .WithColumn("IdUserParticipant").AsInt64().ForeignKey("User", "Id").NotNullable()
                .WithColumn("IdAttestationModule").AsInt64().ForeignKey("AttestationModule", "Id").NotNullable()
                .WithColumn("IdAttestationQuestion").AsInt64().ForeignKey("AttestationQuestion", "Id").NotNullable()
                .WithColumn("IdAttestationAnswer").AsInt64().Nullable()
                .WithColumn("TextAnswer").AsString(255).Nullable();
        }
        public override void Down()
        {
            Delete.Table("UserAnswer");
        }
    }
}
