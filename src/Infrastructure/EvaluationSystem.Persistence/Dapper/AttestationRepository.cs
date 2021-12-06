using Dapper;
using System.Linq;
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

        public List<GetAttestationDto> GetAll()
        {
            string query = @"SELECT a.Id AS IdAttestation, f.[Name] AS FormName, a.CreateDate, u.[Name] AS UsernameToEvaluate, ap.[Status], up.[Name] AS UsernameParticipant
                                    FROM Attestation AS a
                                    JOIN [User] AS u ON u.Id = a.IdUserToEvaluate
                                    JOIN FormTemplate AS f ON f.Id = a.IdForm
                                    JOIN AttestationParticipant ap ON ap.IdAttestation = a.Id
                                    JOIN [User] up ON up.Id = ap.IdUserParticipant";

            var attestationDictionary = new Dictionary<int, GetAttestationDto>();

            var attestations = Connection.Query<GetAttestationDto, string, GetAttestationDto>(query, (attestation, userParticipant) =>
            {
                if (!attestationDictionary.TryGetValue(attestation.IdAttestation, out var currentAttestation))
                {
                    currentAttestation = attestation;
                    attestationDictionary.Add(currentAttestation.IdAttestation, currentAttestation);
                }

                currentAttestation.UsernameParticipant.Add(userParticipant);
                return currentAttestation;
            }, null, Transaction,
               splitOn: "IdAttestation, FormName, CreateDate, UsernameToEvaluate, Status, UsernameParticipant")
            .Distinct()
            .ToList();

            return attestations;
        }

        public GetAttestationDto GetById(int id)
        {
            var query = @"SELECT a.Id AS IdAttestation, f.[Name] AS FormName, a.CreateDate, u.[Name] AS UsernameToEvaluate, ap.[Status], up.[Name] AS UsernameParticipant
                                    FROM Attestation AS a
                                    JOIN [User] AS u ON u.Id = a.IdUserToEvaluate
                                    JOIN FormTemplate AS f ON f.Id = a.IdForm
                                    JOIN AttestationParticipant ap ON ap.IdAttestation = a.Id
                                    JOIN [User] up ON up.Id = ap.IdUserParticipant
                                    WHERE IdAttestation = @Id";

            var attestationDictionary = new Dictionary<int, GetAttestationDto>();

            var attestations = Connection.Query<GetAttestationDto, string, GetAttestationDto>(query, (attestation, userParticipant) =>
            {
                if (!attestationDictionary.TryGetValue(attestation.IdAttestation, out var currentAttestation))
                {
                    currentAttestation = attestation;
                    attestationDictionary.Add(currentAttestation.IdAttestation, currentAttestation);
                }

                currentAttestation.UsernameParticipant.Add(userParticipant);
                return currentAttestation;
            }, new { Id = id }, Transaction,
               splitOn: "IdAttestation, FormName, CreateDate, UsernameToEvaluate, Status, UsernameParticipant")
            .Distinct()
            .ToList();

            return attestations.FirstOrDefault();
        }

        public void DeleteFromRepo(int id)
        {
            string deleteAttestationParticipants = @"DELETE FROM [AttestationParticipant] WHERE IdAttestation = @Id";
            Connection.Execute(deleteAttestationParticipants, new { Id = id }, Transaction);

            Connection.Delete<Attestation>(id, Transaction);
        }
    }
}
