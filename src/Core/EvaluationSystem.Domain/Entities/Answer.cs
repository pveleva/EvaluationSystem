using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int QuestionId { get; set; }
    }
}
