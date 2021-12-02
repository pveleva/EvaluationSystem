using System.Collections.Generic;
using EvaluationSystem.Application.Answers;
using Type = EvaluationSystem.Domain.Entities.Type;

namespace EvaluationSystem.Application.Questions
{
    public class CreateModuleQuestionDto
    {
        public int IdModule { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public int Position { get; set; }
        public ICollection<CreateUpdateAnswerDto> AnswerText { get; set; }
    }
}
