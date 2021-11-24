using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IFormModule;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Persistence.Dapper
{
    public class FormModuleRepository : BaseRepository<FormModule>, IFormModuleRepository
    {
        public FormModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
