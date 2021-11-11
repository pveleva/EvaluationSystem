using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111111023)]
    public class CreateQuestionData : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("QuestionTemplate").Row(new { Name = "Whom would you like to evaluate?", Type = 1, IsReusable = true });
            Insert.IntoTable("QuestionTemplate").Row(new { Name = "How likely is it that you would recommend your coworker to a colleague?", Type = 2, IsReusable = true });
            Insert.IntoTable("QuestionTemplate").Row(new { Name = "How often is your coworker late to work?", Type = 3, IsReusable = true });
            Insert.IntoTable("QuestionTemplate").Row(new { Name = "In a typical week are the hours clocked by your coworker greater than, fewer than, or about the same as the hours actually worked?", Type = 1, IsReusable = true });
            Insert.IntoTable("QuestionTemplate").Row(new { Name = "How much attention to detail does your coworker have?", Type = 4, IsReusable = true });

        }
        public override void Down()
        {
            Delete.FromTable("QuestionTemplate").AllRows();
        }
    }
}
