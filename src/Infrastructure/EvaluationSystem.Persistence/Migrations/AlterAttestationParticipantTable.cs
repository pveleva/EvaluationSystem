using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112131631)]
    public class AlterAttestationParticipantTable : Migration
    {
        public override void Up()
        {
            Alter.Table("AttestationParticipant")
                .AddColumn("Position")
                .AsString(255).Nullable();
        }

        public override void Down()
        {
            Delete.Column("Position").FromTable("AttestationParticipant");
        }
    }
}
