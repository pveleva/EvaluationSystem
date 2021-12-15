using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151605)]
    public class AddAttestationModuleQuestionTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationModuleQuestion")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("IdModule").AsInt64().ForeignKey("AttestationModule", "Id").NotNullable()
                  .WithColumn("IdQuestion").AsInt64().ForeignKey("AttestationQuestion", "Id").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("AttestationModuleQuestion");
        }
    }
}
