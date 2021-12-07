namespace EvaluationSystem.Domain.Entities
{
    public class AttestationAnswer
    {
        public int Id { get; set; }
        public int IdAttestation { get; set; }
        public int IdUserParticipant { get; set; }
        public int IdModuleTemplate { get; set; }
        public int IdQuestionTemplate { get; set; }
        public int IdAnswerTemplate { get; set; }
        public string AnswerText { get; set; }
    }
}
