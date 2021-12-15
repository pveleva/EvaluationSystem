using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151600)]
    public class AddAttestationFormTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationForm")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("[Name]").AsString(1024).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("AttestationForm");
        }
    }
}
