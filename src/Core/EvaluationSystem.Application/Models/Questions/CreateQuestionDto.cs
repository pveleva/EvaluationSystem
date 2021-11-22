using EvaluationSystem.Application.Answers;
using System.Collections.Generic;
using Type = EvaluationSystem.Domain.Entities.Type;

namespace EvaluationSystem.Application.Questions
{
    public class CreateQuestionDto
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public byte IsReusable { get; set; }
        public ICollection<CreateUpdateAnswerDto> AnswerText { get; set; }
    }
}
