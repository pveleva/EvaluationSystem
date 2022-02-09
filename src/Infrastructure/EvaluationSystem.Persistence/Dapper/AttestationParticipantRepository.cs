using Dapper;
using EvaluationSystem.Domain.Enums;
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
        public void UpdateFromRepo(int attestationId, int userId)
        {
            string query = "UPDATE AttestationParticipant SET[Status] = @Status WHERE IdAttestation = @IdAttestation AND IdUserParticipant = @IdUserParticipant;";
            Connection.Execute(query, new { IdAttestation = attestationId, IdUserParticipant = userId, Status = Status.Done }, Transaction);
        }
    }
}
