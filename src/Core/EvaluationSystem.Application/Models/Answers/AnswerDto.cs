namespace EvaluationSystem.Application.Answers
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public byte IsDefault { get; set; }
        public int Position { get; set; } = 1;
        public string AnswerText { get; set; }
        public int IdQuestion { get; set; }
    }
}
