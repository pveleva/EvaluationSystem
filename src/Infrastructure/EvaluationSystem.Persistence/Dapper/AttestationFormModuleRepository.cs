using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestationFormModule;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationFormModuleRepository : BaseRepository<AttestationFormModule>, IAttestationFormModuleRepository
    {
        public AttestationFormModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
