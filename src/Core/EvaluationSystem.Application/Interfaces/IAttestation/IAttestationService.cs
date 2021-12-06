using System.Collections.Generic;
using EvaluationSystem.Application.Models.Attestations;

namespace EvaluationSystem.Application.Interfaces.IAttestation
{
    public interface IAttestationService
    {
        public List<GetAttestationDto> GetAll();
        public GetAttestationDto GetById(int id);
        public GetAttestationDto Create(CreateAttestationDto createAttestationDto);
        public void DeleteFromRepo(int id);
    }
}
