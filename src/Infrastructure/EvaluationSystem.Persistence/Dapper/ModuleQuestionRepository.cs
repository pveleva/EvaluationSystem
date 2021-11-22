using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Persistence.Dapper
{
    public class ModuleQuestionRepository : BaseRepository<ModuleQuestion>, IModuleQuestionRepository
    {
        public ModuleQuestionRepository(IConfiguration configuration)
       : base(configuration)
        {
        }
    }
}
