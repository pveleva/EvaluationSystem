namespace EvaluationSystem.Application.Models.AttestationAnswers
{
    public class UpdateAttestationAnswerDto
    {
        public int IdAttestation { get; set; }
        public int IdAttestationModule { get; set; }
        public int IdAttestationQuestion { get; set; }
        public int IdAttestationAnswer { get; set; }
        public string AnswerText { get; set; }
    }
}
