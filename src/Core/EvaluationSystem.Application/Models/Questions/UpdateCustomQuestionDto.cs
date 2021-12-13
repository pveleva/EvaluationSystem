using Type = EvaluationSystem.Domain.Entities.Type;

namespace EvaluationSystem.Application.Questions
{
    public class UpdateCustomQuestionDto
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public int Position { get; set; }
    }
}
