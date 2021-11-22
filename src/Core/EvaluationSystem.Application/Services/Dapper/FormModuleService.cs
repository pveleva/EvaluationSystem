using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IFormModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class FormModuleService : IFormModuleService
    {
        private IFormModuleRepository _formModuleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FormModuleService(IFormModuleRepository formModuleRepository, IUnitOfWork unitOfWork)
        {
            _formModuleRepository = formModuleRepository;
            _unitOfWork = unitOfWork;
        }

        public void SetModule(FormModule formModule)
        {
            using (_unitOfWork)
            {
                _formModuleRepository.Create(formModule);

                _unitOfWork.Commit();
            }
        }
    }
}
