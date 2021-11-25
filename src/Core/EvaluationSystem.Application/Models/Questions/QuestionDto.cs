using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;

namespace EvaluationSystem.Application.Questions
{
    public class QuestionDto
    {
        public int IdModule { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public int Position { get; set; }
        public IEnumerable<AnswerDto> AnswerText { get; set; }
    }
}
