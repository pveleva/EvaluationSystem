using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112151610)]
    public class AddAttestationAnswerTable : Migration
    {
        public override void Up()
        {
            Create.Table("AttestationAnswer")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("IsDefault").AsBoolean()
                    .WithColumn("Position").AsInt64().NotNullable()
                    .WithColumn("AnswerText").AsString(255).NotNullable()
                    .WithColumn("IdQuestion").AsInt64().ForeignKey("AttestationQuestion", "Id").NotNullable();
        }
        public override void Down()
        {
            Delete.Table("AttestationAnswer");
        }
    }
}
