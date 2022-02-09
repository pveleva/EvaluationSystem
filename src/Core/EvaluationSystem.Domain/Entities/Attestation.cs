using System;

namespace EvaluationSystem.Domain.Entities
{
    public class Attestation:BaseEntity
    {
        public int IdForm { get; set; }
        public int IdUserToEvaluate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
