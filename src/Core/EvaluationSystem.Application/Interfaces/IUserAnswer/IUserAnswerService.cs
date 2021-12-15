using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IUserAnswer
{
    public interface IUserAnswerService
    {
        public CreateGetFormDto Get(int idAttestation, string email);
        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto);
    }
}
