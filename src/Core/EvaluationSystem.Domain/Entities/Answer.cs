namespace EvaluationSystem.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public byte IsDefault { get; set; }
        public int Position { get; set; }
        public string AnswerText { get; set; }
        public int IdQuestion { get; set; }
    }
}
