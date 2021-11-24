using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Persistence.Dapper
{
    public class ModuleQuestionRepository : BaseRepository<ModuleQuestion>, IModuleQuestionRepository
    {
        public ModuleQuestionRepository(IUnitOfWork unitOfWork)
             : base(unitOfWork)
        {
        }
    }
}
