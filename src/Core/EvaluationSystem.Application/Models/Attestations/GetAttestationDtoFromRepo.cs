using System;
using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class GetAttestationDtoFromRepo
    {
        public int IdAttestation { get; set; }
        public int IdAttestationForm { get; set; }
        public string UsernameToEvaluate { get; set; }
        public string FormName { get; set; }
        public string UsernameParticipant { get; set; }
        public string EmailParticipant { get; set; }
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
