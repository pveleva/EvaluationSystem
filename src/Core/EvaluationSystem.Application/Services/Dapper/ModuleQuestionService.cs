using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class ModuleQuestionService : IModuleQuestionService
    {
        private IModuleQuestionRepository _moduleQuestionRepository;

        public ModuleQuestionService(IModuleQuestionRepository moduleQuestionRepository)
        {
            _moduleQuestionRepository = moduleQuestionRepository;
        }

        public void SetQuestion(ModuleQuestion moduleQuestion)
        {
            _moduleQuestionRepository.Create(moduleQuestion);
        }
    }
}
