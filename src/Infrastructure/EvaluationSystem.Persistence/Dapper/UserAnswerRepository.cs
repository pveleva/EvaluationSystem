using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IUserAnswer;

namespace EvaluationSystem.Persistence.Dapper
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        public UserAnswer IsAttestationAnswerExist(int idAttestation, int idUserParticipant, int idAttestationModule, int idAttestationQuestion, int idAttestationAnswer)
        {
            string query = @"SELECT * FROM UserAnswer
                             WHERE IdAttestation =@IdAttestation AND IdUserParticipant =@IdUserParticipant AND IdAttestationModule =@IdAttestationModule 
                                                                 AND IdAttestationQuestion =@IdAttestationQuestion AND IdAttestationAnswer =@IdAttestationAnswer";
            return Connection.QueryFirstOrDefault<UserAnswer>(query, new
            {
                IdAttestation = idAttestation,
                IdUserParticipant = idUserParticipant,
                IdAttestationModule = idAttestationModule,
                IdAttestationQuestion = idAttestationQuestion,
                IdAttestationAnswer = idAttestationAnswer
            }, Transaction);
        }
        public UserAnswer DeleteAttestationAnswer(int idAttestation, int idUserParticipant, int idAttestationModule, int idAttestationQuestion, int idAttestationAnswer)
        {
            string query = @"DELETE FROM UserAnswer
                             WHERE IdAttestation =@IdAttestation AND IdUserParticipant =@IdUserParticipant AND IdAttestationModule =@IdAttestationModule 
                                                                 AND IdAttestationQuestion =@IdAttestationQuestion AND IdAttestationAnswer =@IdAttestationAnswer";
            return Connection.QueryFirstOrDefault<UserAnswer>(query, new
            {
                IdAttestation = idAttestation,
                IdUserParticipant = idUserParticipant,
                IdAttestationModule = idAttestationModule,
                IdAttestationQuestion = idAttestationQuestion,
                IdAttestationAnswer = idAttestationAnswer
            }, Transaction);
        }
        public UserAnswer DeleteAllAttestationAnswer(int idAttestation, int idUserParticipant, int idAttestationModule, int idAttestationQuestion)
        {
            string query = @"DELETE FROM UserAnswer
                             WHERE IdAttestation =@IdAttestation AND IdUserParticipant =@IdUserParticipant AND IdAttestationModule =@IdAttestationModule 
                                                                 AND IdAttestationQuestion =@IdAttestationQuestion";
            return Connection.QueryFirstOrDefault<UserAnswer>(query, new
            {
                IdAttestation = idAttestation,
                IdUserParticipant = idUserParticipant,
                IdAttestationModule = idAttestationModule,
                IdAttestationQuestion = idAttestationQuestion
            }, Transaction);
        }
    }
}
