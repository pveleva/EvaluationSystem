using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class ModuleQuestionService : IModuleQuestionService
    {
        private IModuleQuestionRepository _moduleQuestionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleQuestionService(IModuleQuestionRepository moduleQuestionRepository, IUnitOfWork unitOfWork)
        {
            _moduleQuestionRepository = moduleQuestionRepository;
            _unitOfWork = unitOfWork;
        }

        public void SetQuestion(ModuleQuestion moduleQuestion)
        {
            using (_unitOfWork)
            {
                _moduleQuestionRepository.Create(moduleQuestion);

                _unitOfWork.Commit();
            }
        }
    }
}
