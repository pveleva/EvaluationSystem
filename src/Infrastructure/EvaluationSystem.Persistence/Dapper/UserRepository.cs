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
            string query = @"SELECT u.[Name] AS Name FROM Attestation AS a
                                    JOIN [User] AS u ON u.Id = a.IdUserToEvaluate
                                    JOIN AttestationParticipant ap ON ap.IdAttestation = a.Id
                                    JOIN [User] up ON up.Id = ap.IdUserParticipant
									WHERE up.Email = @Email";
            return Connection.Query<User>(query, new { Email = email }, Transaction).AsList();
        }
    }
}
