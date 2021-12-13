using Dapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Application.Interfaces.IAttestation;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationRepository : BaseRepository<Attestation>, IAttestationRepository
    {
        public AttestationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<GetAttestationDtoFromRepo> GetAll()
        {
            string query = @"SELECT  a.Id AS IdAttestation, u.[Name] AS UsernameToEvaluate, f.[Name] AS FormName, up.[Name] AS UsernameParticipant, ap.[Status], a.CreateDate
                                     FROM Attestation AS a
                                     JOIN [User] AS u ON u.Id = a.IdUserToEvaluate
                                     JOIN FormTemplate AS f ON f.Id = a.IdForm
                                     JOIN AttestationParticipant ap ON ap.IdAttestation = a.Id
                                     JOIN [User] up ON up.Id = ap.IdUserParticipant";

            return Connection.Query<GetAttestationDtoFromRepo>(query, null, Transaction).AsList();
        }

        public List<GetAttestationDtoFromRepo> GetByEmail(string email)
        {
            var query = @"SELECT  a.Id AS IdAttestation, u.[Name] AS UsernameToEvaluate, f.[Name] AS FormName, up.[Name] AS UsernameParticipant, ap.[Status], a.CreateDate
                                    FROM Attestation AS a
                                    JOIN [User] AS u ON u.Id = a.IdUserToEvaluate
                                    JOIN FormTemplate AS f ON f.Id = a.IdForm
                                    JOIN AttestationParticipant ap ON ap.IdAttestation = a.Id
                                    JOIN [User] up ON up.Id = ap.IdUserParticipant
                                    WHERE [Email] = @Email";

            return Connection.Query<GetAttestationDtoFromRepo>(query, new { Email = email }, Transaction).AsList();
        }

        public void DeleteFromRepo(int id)
        {
            string deleteAttestationParticipants = @"DELETE FROM [AttestationParticipant] WHERE IdAttestation = @Id";
            Connection.Execute(deleteAttestationParticipants, new { Id = id }, Transaction);

            Connection.Delete<Attestation>(id, Transaction);
        }
    }
}
