namespace EvaluationSystem.Application.Answers
{
    public class CreateAnswerDto
    {
        public byte IsDefault { get; set; }
        public int Position { get; set; } = 1;
        public string AnswerText { get; set; }
    }
}
