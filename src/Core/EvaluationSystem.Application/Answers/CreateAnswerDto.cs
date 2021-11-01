using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Answers
{
    public class CreateAnswerDto
    {
        public string Content { get; set; }
        public int QuestionId { get; set; }
    }
}
