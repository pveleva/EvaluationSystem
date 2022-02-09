using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Models.AttestationAnswers
{
    public class GetAttestationAnswerDto
    {
        public string Username { get; set; }
        public GetFormModuleQuestionAnswerDto FormModuleQuestionAnswerDto { get; set; }
    }
}
