using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IFormModule;

namespace EvaluationSystem.Persistence.Dapper
{
    public class FormModuleRepository : BaseRepository<FormModule>, IFormModuleRepository
    {
        public FormModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        public void UpdateFromRepo(int formId, int moduleId, int position)
        {
            string query = @"UPDATE FormModule SET Position = @Position WHERE IdForm = @IdForm AND IdModule = @IdModule;";
            Connection.Query<FormModule>(query, new { IdForm = formId, IdModule = moduleId, Position = position }, Transaction).AsList();
        }
    }
}
