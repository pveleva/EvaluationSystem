using EvaluationSystem.Application.Answers;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Questions
{
    public class QuestionDto
    {
        public string Content { get; set; }
        public Domain.Entities.Type Type { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
