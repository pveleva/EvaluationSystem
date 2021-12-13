using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Users
{
    public class ExposeUserParticipantDto
    {
        public string UsernameToEvaluate { get; set; }
        public string UsernameParticipant { get; set; }
        public Status Status { get; set; }
    }
}
