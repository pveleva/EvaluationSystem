namespace EvaluationSystem.Application.Answers
{
    public class CreateUpdateAnswerDto
    {
        public bool IsDefault { get; set; }
        public int Position { get; set; } = 1;
        public string AnswerText { get; set; }
    }
}
