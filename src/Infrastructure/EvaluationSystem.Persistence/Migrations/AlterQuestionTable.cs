using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112131734)]
    public class AlterQuestionTable : Migration
    {
        public override void Up()
        {
            Alter.Table("QuestionTemplate")
                .AddColumn("DateOfCreation")
                .AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Column("DateOfCreation").FromTable("QuestionTemplate");
        }
    }
}
