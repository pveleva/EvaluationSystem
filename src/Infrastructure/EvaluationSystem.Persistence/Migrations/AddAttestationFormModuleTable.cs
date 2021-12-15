using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151604)]
    public class AddAttestationFormModuleTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationFormModule")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("IdForm").AsInt64().ForeignKey("AttestationForm", "Id").NotNullable()
                  .WithColumn("IdModule").AsInt64().ForeignKey("AttestationModule", "Id").NotNullable()
                  .WithColumn("Position").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("AttestationFormModule");
        }
    }
}
