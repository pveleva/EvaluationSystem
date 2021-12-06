using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Domain.Entities
{
    public class AttestationParticipant : BaseEntity
    {
        public int IdAttestation { get; set; }
        public int IdUserParticipant { get; set; }
        public Status Status { get; set; }
    }
}
