using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Persistence.Dapper
{
    public class ModuleQuestionRepository : BaseRepository<ModuleQuestion>, IModuleQuestionRepository
    {
        public ModuleQuestionRepository(IUnitOfWork unitOfWork)
             : base(unitOfWork)
        {
        }

        public void UpdateFromRepo (int moduleId, int questionId, int position)
        {
            string query = @"UPDATE ModuleQuestion SET Position = @Position WHERE IdModule = @IdModule AND IdQuestion = @IdQuestion;";
            Connection.Query<ModuleQuestion>(query, new { IdModule = moduleId, IdQuestion = questionId, Position = position }, Transaction).AsList();
        }
    }
}
