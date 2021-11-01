using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Answers
{
    public class UpdateAnswerDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int QuestionId { get; set; }
    }
}
