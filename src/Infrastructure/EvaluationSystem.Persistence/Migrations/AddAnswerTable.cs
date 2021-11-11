using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101537)]
    public class AddAnswerTable : Migration
    {
        public override void Up()
        {
            Create.Table("AnswerTemplate")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("IsDefault").AsBinary().NotNullable()
                .WithColumn("Position").AsInt64().NotNullable()
                .WithColumn("AnswerText").AsString(255).NotNullable()
                .WithColumn("IdQuestion").AsInt64().ForeignKey().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("AnswerTemplate");
        }
    }
}
