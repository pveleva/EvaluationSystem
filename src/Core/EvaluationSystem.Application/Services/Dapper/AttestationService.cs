using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Application.Interfaces.IAttestation;
using EvaluationSystem.Application.Interfaces.IAttestationParticipant;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationService : IAttestationService, IExceptionService
    {
        private readonly IAttestationRepository _attestationRepository;
        private readonly IAttestationParticipantRepository _attestationParticipantRepository;
        private readonly IFormRepository _formRepository;
        private readonly IUserRepository _userRepository;
        public AttestationService(IAttestationRepository attestationRepository, IAttestationParticipantRepository attestationParticipantRepository, IFormRepository formRepository, IUserRepository userRepository)
        {
            _attestationRepository = attestationRepository;
            _attestationParticipantRepository = attestationParticipantRepository;
            _formRepository = formRepository;
            _userRepository = userRepository;
        }
        public List<GetAttestationDto> GetAll()
        {
            return _attestationRepository.GetAll();
        }
        public GetAttestationDto GetById(int id)
        {
            ThrowExceptionWhenEntityDoNotExist(id, _attestationRepository);
            return _attestationRepository.GetById(id);
        }
        public GetAttestationDto Create(CreateAttestationDto createAttestationDto)
        {
            ThrowExceptionWhenEntityDoNotExist(createAttestationDto.IdForm, _formRepository);
            ThrowExceptionWhenEntityDoNotExist(createAttestationDto.IdUserToEvaluate, _userRepository);

            foreach (var participant in createAttestationDto.IdUserParticipant)
            {
                ThrowExceptionWhenEntityDoNotExist(participant, _userRepository);
            }

            int attestationId = _attestationRepository.Create(new Attestation()
            {
                IdForm = createAttestationDto.IdForm,
                IdUserToEvaluate = createAttestationDto.IdUserToEvaluate,
                CreateDate = DateTime.Now
            });

            foreach (var participant in createAttestationDto.IdUserParticipant)
            {
                _attestationParticipantRepository.Create(new AttestationParticipant()
                {
                    IdAttestation = attestationId,
                    IdUserParticipant = participant,
                    Status = createAttestationDto.Status
                });
            }

            return GetById(attestationId);
        }

        public void DeleteFromRepo(int id)
        {
            _attestationRepository.DeleteFromRepo(id);
        }

        public void ThrowExceptionWhenEntityDoNotExist<T>(int id, IGenericRepository<T> repository)
        {
            var entity = repository.GetByID(id);

            var entityName = "";
            if (typeof(T).Name == "User")
            {
                entityName = "User";
            }
            else
            {
                entityName = typeof(T).Name.Remove(typeof(T).Name.Length - 8);
            }

            if (entity == null)
            {
                throw new NullReferenceException($"{entityName} with ID:{id} doesn't exist!");
            }
        }
    }
}
