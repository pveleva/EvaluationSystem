using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Questions
{
    public class QuestionDto
    {
        public string Content { get; set; }
        public Domain.Entities.Type Type { get; set; }
        public ICollection<Answer> IAnswers { get; private set; }
    }
}
