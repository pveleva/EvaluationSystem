using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces.IAttestationFormModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationFormModuleService : IAttestationFormModuleService
    {
        private IAttestationFormModuleRepository _formModuleRepository;

        public AttestationFormModuleService(IAttestationFormModuleRepository formModuleRepository)
        {
            _formModuleRepository = formModuleRepository;
        }

        public void SetModule(AttestationFormModule formModule)
        {
            _formModuleRepository.Create(formModule);
        }
    }
}