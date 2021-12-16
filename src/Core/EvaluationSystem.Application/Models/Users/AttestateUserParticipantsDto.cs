using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Users
{
    public class AttestateUserParticipantsDto
    {
        public string ParticipantName { get; set; }
        public string ParticipantEmail { get; set; }
        public ParticipantPosition Position { get; set; }
    }
}
