using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112021629)]
    public class AddAttestationParticipantTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationParticipant")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("IdAttestation").AsInt64().ForeignKey("Attestation", "Id").NotNullable()
                .WithColumn("IdUserParticipant").AsInt64().ForeignKey("User", "Id").NotNullable()
                .WithColumn("Status").AsString(255).NotNullable();
        }
        public override void Down()
        {
            Delete.Table("AttestationParticipant");
        }
    }
}
