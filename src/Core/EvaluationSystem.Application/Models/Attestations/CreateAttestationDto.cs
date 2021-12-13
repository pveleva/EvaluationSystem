using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class CreateAttestationDto
    {
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public int IdForm { get; set; }
        public ICollection<AttestateUserParticipantsDto> UserParticipants { get; set; }
    }
}
