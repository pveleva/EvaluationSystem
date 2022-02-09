using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces.IAttestationModuleQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationModuleQuestionService : IAttestationModuleQuestionService
    {
        private IAttestationModuleQuestionRepository _moduleQuestionRepository;

        public AttestationModuleQuestionService(IAttestationModuleQuestionRepository moduleQuestionRepository)
        {
            _moduleQuestionRepository = moduleQuestionRepository;
        }

        public void SetQuestion(AttestationModuleQuestion moduleQuestion)
        {
            _moduleQuestionRepository.Create(moduleQuestion);
        }
    }
}