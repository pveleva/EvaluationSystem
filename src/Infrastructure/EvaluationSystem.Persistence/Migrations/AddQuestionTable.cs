﻿using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111191342)]
    public class AddQuestionTable : Migration
    {
        public override void Up()
        {
            Create.Table("QuestionTemplate")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("[Name]").AsString(1024).NotNullable()
                  .WithColumn("[Type]").AsString(20).NotNullable()
                  .WithColumn("IsReusable").AsBoolean()
                  .WithColumn("DateOfCreation").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("QuestionTemplate");
        }
    }
}
