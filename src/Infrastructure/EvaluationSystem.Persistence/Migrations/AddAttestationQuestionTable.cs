using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151603)]
    public class AddAttestationQuestionTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationQuestion")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("[Name]").AsString(1024).NotNullable()
                  .WithColumn("[Type]").AsString(20).NotNullable()
                  .WithColumn("IsReusable").AsBoolean()
                  .WithColumn("DateOfCreation").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("AttestationQuestion");
        }
    }
}
