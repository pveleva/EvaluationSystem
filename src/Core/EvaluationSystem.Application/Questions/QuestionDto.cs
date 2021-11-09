using System.Collections.Generic;

namespace EvaluationSystem.Application.Questions
{
    public class QuestionDto
    {
        public string Name { get; set; }

        public ICollection<string> AnswerText { get; set; }
    }
}
