using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class CreateAttestationDto
    {
        public int IdForm { get; set; }
        public int IdUserToEvaluate { get; set; }
        public ICollection<int> IdUserParticipant { get; set; }
        public Status Status { get; set; }
    }
}
