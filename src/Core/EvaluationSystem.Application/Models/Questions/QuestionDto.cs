using EvaluationSystem.Application.Answers;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Questions
{
    public class QuestionDto
    {
        public int IdModule { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<AnswerDto> AnswerText { get; set; }
    }
}
