namespace EvaluationSystem.Domain.Entities
{
    public class AttestationModuleQuestion : BaseEntity
    {
        public int IdModule { get; set; }
        public int IdQuestion { get; set; }
        public int Position { get; set; }
    }
}
