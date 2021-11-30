namespace EvaluationSystem.Application.Answers
{
    public class CreateAnswerDto
    {
        public byte IsDefault { get; set; }
        public int Position { get; set; }
        public string AnswerText { get; set; }
    }
}
