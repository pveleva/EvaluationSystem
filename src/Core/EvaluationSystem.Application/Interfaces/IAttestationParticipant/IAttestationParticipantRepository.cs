using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces.IAttestationParticipant
{
    public interface IAttestationParticipantRepository : IGenericRepository<AttestationParticipant>
    {
        public void UpdateFromRepo(int attestationId, int userId);
    }
}
