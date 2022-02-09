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
            string query = @"SELECT  att.Id AS IdAttestation, f.Id AS IdAttestationForm, u.[Name] AS UsernameToEvaluate, f.[Name] AS FormName, up.[Name] AS UsernameParticipant, 
                                    up.[Email] AS EmailParticipant, ap.[Status], att.CreateDate
                             FROM Attestation AS att
                             RIGHT JOIN [User] AS u ON u.Id = att.IdUserToEvaluate
                             RIGHT JOIN AttestationForm AS f ON f.Id = att.IdForm
                             RIGHT JOIN  AttestationParticipant ap ON ap.IdAttestation = att.Id
                             LEFT JOIN  [User] up ON up.Id = ap.IdUserParticipant";
            
            return Connection.Query<GetAttestationDtoFromRepo>(query, null, Transaction).AsList();
        }

        public void DeleteFromRepo(int id)
        {
            string deleteAttestationParticipants = @"DELETE FROM [AttestationParticipant] WHERE IdAttestation = @Id";
            Connection.Execute(deleteAttestationParticipants, new { Id = id }, Transaction);

            string deleteAttestationAnswers = @"DELETE FROM [UserAnswer] WHERE IdAttestation = @Id";
            Connection.Execute(deleteAttestationAnswers, new { Id = id }, Transaction);

            Connection.Delete<Attestation>(id, Transaction);
        }
    }
}
