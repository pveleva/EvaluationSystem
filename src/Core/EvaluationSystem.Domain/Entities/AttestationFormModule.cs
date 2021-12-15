namespace EvaluationSystem.Domain.Entities
{
    public class AttestationFormModule : BaseEntity
    {
        public int IdForm { get; set; }
        public int IdModule { get; set; }
        public int Position { get; set; }
    }
}
