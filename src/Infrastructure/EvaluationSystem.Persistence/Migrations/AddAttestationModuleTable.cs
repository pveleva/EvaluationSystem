using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151601)]
    public class AddAttestationModuleTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationModule")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("[Name]").AsString(1024).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("AttestationModule");
        }
    }
}
