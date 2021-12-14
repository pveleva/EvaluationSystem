using Dapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IUser;

namespace EvaluationSystem.Persistence.Dapper
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<User> GetUsersToEvaluate(string email)
        {
            string query = @"SELECT u.[Name] FROM [USER] AS u 
									JOIN [Attestation] AS a ON u.Id = a.IdUserToEvaluate
									JOIN [AttestationParticipant] AS ap ON a.Id = ap.IdAttestation
									JOIN [User] AS ue ON ap.IdUserParticipant = ue.Id
									WHERE ue.Email = @Email AND ap.[Status] = 1";
            return Connection.Query<User>(query, new { Email = email }, Transaction).AsList();
        }
    }
}
