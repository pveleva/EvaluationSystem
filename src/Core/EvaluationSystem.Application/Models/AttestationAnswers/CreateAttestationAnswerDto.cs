using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.AttestationAnswers
{
    public class CreateAttestationAnswerDto
    {
        public int IdAttestation { get; set; }
        public List<CreateAttestationAnswerBodyDto> AttestationAnswerBodyDtos { get; set; }
    }
}
