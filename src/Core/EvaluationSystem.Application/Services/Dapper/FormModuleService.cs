using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces.IFormModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class FormModuleService : IFormModuleService
    {
        private IFormModuleRepository _formModuleRepository;

        public FormModuleService(IFormModuleRepository formModuleRepository)
        {
            _formModuleRepository = formModuleRepository;
        }

        public void SetModule(FormModule formModule)
        {
            _formModuleRepository.Create(formModule);
        }
    }
}
