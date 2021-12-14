using EvaluationSystem.Application.Models.AttestationAnswers;

namespace EvaluationSystem.Application.Interfaces.IAttestationAnswer
{
    public interface IAttestationAnswerService
    {
        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto);
    }
}
