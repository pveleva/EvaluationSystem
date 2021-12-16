using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112021619)]
    public class AddAttestationTable : Migration
    {
        public override void Up()
        {
            Create.Table("Attestation")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("IdForm").AsInt64().ForeignKey("AttestationForm", "Id").NotNullable()
                .WithColumn("IdUserToEvaluate").AsInt64().ForeignKey("User", "Id").NotNullable()
                .WithColumn("CreateDate").AsDateTime2().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Attestation");
        }
    }
}
