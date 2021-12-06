using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestationParticipant;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationParticipantRepository : BaseRepository<AttestationParticipant>, IAttestationParticipantRepository
    {
        public AttestationParticipantRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
