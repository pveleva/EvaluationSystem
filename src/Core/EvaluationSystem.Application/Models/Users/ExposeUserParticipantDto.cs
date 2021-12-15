using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Users
{
    public class ExposeUserParticipantDto
    {
        public int IdAttestation { get; set; }
        public string UsernameParticipant { get; set; }
        public Status Status { get; set; }
    }
}
