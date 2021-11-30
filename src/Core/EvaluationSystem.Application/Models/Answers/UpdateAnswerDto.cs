namespace EvaluationSystem.Application.Answers
{
    public class UpdateAnswerDto
    {
        public byte IsDefault { get; set; }
        public int Position { get; set; }
        public string AnswerText { get; set; }
    }
}
