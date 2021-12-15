using System;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Interfaces.IForm;
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
        private readonly IUser _currentUser;
        public AttestationService(IAttestationRepository attestationRepository, IAttestationParticipantRepository attestationParticipantRepository,
            IFormRepository formRepository, IUserRepository userRepository, IUser currentUser)
        {
            _attestationRepository = attestationRepository;
            _attestationParticipantRepository = attestationParticipantRepository;
            _formRepository = formRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }
        public List<GetAttestationDto> GetAll()
        {
            List<GetAttestationDtoFromRepo> attestationsRepo = _attestationRepository.GetAll();

            List<GetAttestationDto> attestations = attestationsRepo.GroupBy(x => new { x.IdAttestation, x.UsernameToEvaluate, x.FormName, x.CreateDate })
                    .Select(q => new GetAttestationDto()
                    {
                        IdAttestation = q.Key.IdAttestation,
                        UsernameToEvaluate = q.Key.UsernameToEvaluate,
                        FormName = q.Key.FormName,
                        Participants = new List<ExposeUserParticipantDto>(),
                        CreateDate = q.Key.CreateDate
                    }).ToList();

            List<ExposeUserParticipantDto> participants = attestationsRepo.GroupBy(x => new { x.IdAttestation, x.UsernameParticipant, x.Status })
                    .Select(q => new ExposeUserParticipantDto()
                    {
                        IdAttestation = q.Key.IdAttestation,
                        UsernameParticipant = q.Key.UsernameParticipant,
                        Status = q.Key.Status
                    }).ToList();

            foreach (var attestation in attestations)
            {
                attestation.Participants = participants.Where(ute => ute.IdAttestation == attestation.IdAttestation).ToList();
                if (attestation.Participants.All(p => p.Status == Domain.Enums.Status.Done))
                {
                    attestation.Status = Domain.Enums.Status.Done;
                }
                else if (attestation.Participants.All(p => p.Status == Domain.Enums.Status.Open))
                {
                    attestation.Status = Domain.Enums.Status.Open;
                }
                else
                {
                    attestation.Status = Domain.Enums.Status.InProgress;
                }
            }

            return attestations;
        }

        public GetAttestationDto GetByEmail()
        {
            List<GetAttestationDtoFromRepo> attestationsRepo = _attestationRepository.GetByEmail(_currentUser.Email);

            List<GetAttestationDto> attestations = attestationsRepo.GroupBy(x => new { x.IdAttestation, x.UsernameToEvaluate, x.FormName, x.CreateDate })
                    .Select(q => new GetAttestationDto()
                    {
                        IdAttestation = q.Key.IdAttestation,
                        UsernameToEvaluate = q.Key.UsernameToEvaluate,
                        FormName = q.Key.FormName,
                        Participants = new List<ExposeUserParticipantDto>(),
                        CreateDate = q.Key.CreateDate
                    }).ToList();

            List<ExposeUserParticipantDto> participants = attestationsRepo.GroupBy(x => new { x.IdAttestation, x.UsernameParticipant, x.Status })
                    .Select(q => new ExposeUserParticipantDto()
                    {
                        IdAttestation = q.Key.IdAttestation,
                        UsernameParticipant = q.Key.UsernameParticipant,
                        Status = q.Key.Status
                    }).ToList();

            foreach (var attestation in attestations)
            {
                attestation.Participants = participants.Where(ute => ute.IdAttestation == attestation.IdAttestation).ToList();
                if (attestation.Participants.All(p => p.Status == Domain.Enums.Status.Done))
                {
                    attestation.Status = Domain.Enums.Status.Done;
                }
                else if (attestation.Participants.All(p => p.Status == Domain.Enums.Status.Open))
                {
                    attestation.Status = Domain.Enums.Status.Open;
                }
                else
                {
                    attestation.Status = Domain.Enums.Status.InProgress;
                }
            }

            return attestations.FirstOrDefault();
        }

        public GetAttestationDto Create(CreateAttestationDto createAttestationDto)
        {
            ThrowExceptionWhenEntityDoNotExist(createAttestationDto.IdForm, _formRepository);
            int idUserToEvaluate = 0;
            int idUserParticipant = 0;

            var userToEvaluate = _userRepository.GetList().Where(u => u.Name == createAttestationDto.Username).FirstOrDefault();

            if (userToEvaluate == null)
            {
                idUserToEvaluate = _userRepository.Create(new User { Name = createAttestationDto.Username, Email = createAttestationDto.UserEmail });
            }
            else
            {
                idUserToEvaluate = userToEvaluate.Id;
            }

            int attestationId = _attestationRepository.Create(new Attestation()
            {
                IdForm = createAttestationDto.IdForm,
                IdUserToEvaluate = idUserToEvaluate,
                CreateDate = DateTime.Now
            });

            foreach (var participant in createAttestationDto.UserParticipants)
            {
                var userParticipant = _userRepository.GetList().Where(u => u.Name == participant.ParticipantName).FirstOrDefault();
                if (userParticipant == null)
                {
                    idUserParticipant = _userRepository.Create(new User { Name = participant.ParticipantName, Email = participant.ParticipantEmail });
                }
                else
                {
                    idUserParticipant = userParticipant.Id;
                }

                _attestationParticipantRepository.Create(new AttestationParticipant()
                {
                    IdAttestation = attestationId,
                    IdUserParticipant = idUserParticipant,
                    Status = Domain.Enums.Status.Open,
                    Position = participant.Position
                });
            }

            return GetAll().Where(u => u.UsernameToEvaluate == createAttestationDto.Username).FirstOrDefault();
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
