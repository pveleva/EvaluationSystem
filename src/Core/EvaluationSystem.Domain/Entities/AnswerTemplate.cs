namespace EvaluationSystem.Domain.Entities
{
    public class AnswerTemplate : BaseEntity
    {
        public bool IsDefault { get; set; }
        public int Position { get; set; }
        public string AnswerText { get; set; }
        public int IdQuestion { get; set; }
    }
}
