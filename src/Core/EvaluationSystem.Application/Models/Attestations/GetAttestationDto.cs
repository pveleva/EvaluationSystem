using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class GetAttestationDto
    {
        public GetAttestationDto()
        {
            this.Participants = new HashSet<ExposeUserParticipantDto>();
        }
        public int IdAttestation { get; set; }
        public int IdAttestationForm { get; set; }
        public string UsernameToEvaluate { get; set; }
        public string FormName { get; set; }
        public Status Status { get; set; }
        public ICollection<ExposeUserParticipantDto> Participants { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
