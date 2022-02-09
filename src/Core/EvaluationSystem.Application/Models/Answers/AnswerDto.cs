namespace EvaluationSystem.Application.Answers
{
    public class AnswerDto
    {
        public int IdQuestion { get; set; }
        public int Id { get; set; }
        public byte IsDefault { get; set; } = 0;
        public int Position { get; set; } = 1;
        public string AnswerText { get; set; }
        public byte IsAnswered { get; set; } = 0;
    }
}
