using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101543)]
    public class AddQuestionTable : Migration
    {
        public override void Up()
        {
            Create.Table("QuestionTemplate")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("[Name]").AsString(1024).NotNullable()
                  .WithColumn("[Type]").AsString(20).NotNullable()
                  .WithColumn("IsReusable").AsBinary(255).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("QuestionTemplate");
        }
    }
}
