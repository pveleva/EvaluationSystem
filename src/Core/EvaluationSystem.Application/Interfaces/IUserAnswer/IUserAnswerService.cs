using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IUserAnswer
{
    public interface IUserAnswerService
    {
        public CreateGetFormDto Get(int idAttestation, string email);
        public void Update(UpdateAttestationAnswerDto updateAttestationAnswerDto);
        public void Create(CreateAttestationAnswerDto createAttestationAnswerDto);
    }
}
