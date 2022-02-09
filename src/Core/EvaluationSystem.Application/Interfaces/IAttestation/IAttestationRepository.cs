using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Attestations;

namespace EvaluationSystem.Application.Interfaces.IAttestation
{
    public interface IAttestationRepository : IGenericRepository<Attestation>
    {
        public List<GetAttestationDtoFromRepo> GetAll();
        public void DeleteFromRepo(int id);
    }
}
