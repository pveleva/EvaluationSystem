using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces.IUserAnswer
{
    public interface IUserAnswerRepository : IGenericRepository<UserAnswer>
    {
        public UserAnswer IsAttestationAnswerExist(int idAttestation, int idUserParticipant, int idAttestationModule, int idAttestationQuestion, int idAttestationAnswer);
        public UserAnswer DeleteAttestationAnswer(int idAttestation, int idUserParticipant, int idAttestationModule, int idAttestationQuestion, int idAttestationAnswer);
        public UserAnswer DeleteAllAttestationAnswer(int idAttestation, int idUserParticipant, int idAttestationModule, int idAttestationQuestion);
    }
}
