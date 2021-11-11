using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111111037)]
    public class CreateAnswerData : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 1, Position = 1, AnswerText = "Supervisor", IdQuestion = 1 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 2, AnswerText = "Coworker", IdQuestion = 1 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 3, AnswerText = "Subordinate", IdQuestion = 1 });

            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 1, Position = 1, AnswerText = "1", IdQuestion = 2 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 2, AnswerText = "2", IdQuestion = 2 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 3, AnswerText = "3", IdQuestion = 2 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 4, AnswerText = "4", IdQuestion = 2 });

            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 1, Position = 1, AnswerText = "Very good", IdQuestion = 3 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 2, AnswerText = "Good", IdQuestion = 3 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 3, AnswerText = "Not so good", IdQuestion = 3 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 4, AnswerText = "Not at all good", IdQuestion = 3 });

            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 1, Position = 1, AnswerText = "Very productive", IdQuestion = 4 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 2, AnswerText = "Productive", IdQuestion = 4 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 3, AnswerText = "Not so productive", IdQuestion = 4 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 4, AnswerText = "Not at all productive", IdQuestion = 4 });

            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 1, Position = 1, AnswerText = "Extremely well", IdQuestion = 5 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 2, AnswerText = "Very well", IdQuestion = 5 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 3, AnswerText = "Not so well", IdQuestion = 5 });
            Insert.IntoTable("AnswerTemplate").Row(new { IsDefault = 0, Position = 4, AnswerText = "Not at all well", IdQuestion = 5 });
        }
        public override void Down()
        {
            Delete.FromTable("AnswerTemplate").AllRows();
        }
    }
}
