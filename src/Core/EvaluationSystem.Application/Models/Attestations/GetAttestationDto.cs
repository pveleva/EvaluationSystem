using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class GetAttestationDto
    {
        public GetAttestationDto()
        {
            this.UsernameParticipant = new HashSet<string>();
        }
        public int IdAttestation { get; set; }
        public string FormName { get; set; }
        public DateTime CreateDate { get; set; }
        public string UsernameToEvaluate { get; set; }
        public Status Status { get; set; }
        public ICollection<string> UsernameParticipant { get; set; }
    }
}
