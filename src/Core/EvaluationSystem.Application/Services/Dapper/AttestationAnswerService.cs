using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationAnswerService : IAttestationAnswerService, IExceptionService
    {
        private readonly IQuestionService _questionService;
        private readonly IAttestationAnswerRepository _attestationAnswerRepository;
        private readonly IUser _currentUser;
        public AttestationAnswerService(IQuestionService questionService, IAttestationAnswerRepository attestationAnswerRepository, IUser currentUser)
        {
            _questionService = questionService;
            _attestationAnswerRepository = attestationAnswerRepository;
            _currentUser = currentUser;
        }

        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            var attestationAnswer = new AttestationAnswer();

            var questionType = _questionService.GetById(createAttestationAnswerDto.IdQuestionTemplate).Type;

            if (questionType == Domain.Entities.Type.TextField)
            {
                attestationAnswer = new AttestationAnswer()
                {
                    IdAttestation = createAttestationAnswerDto.IdAttestation,
                    IdUserParticipant = _currentUser.Id,
                    IdModuleTemplate = createAttestationAnswerDto.IdModuleTemplate,
                    IdQuestionTemplate = createAttestationAnswerDto.IdQuestionTemplate,
                    IdAnswerTemplate = new List<int>(),
                    AnswerText = createAttestationAnswerDto.AnswerText
                };
            }
            else
            {
                attestationAnswer = new AttestationAnswer()
                {
                    IdAttestation = createAttestationAnswerDto.IdAttestation,
                    IdUserParticipant = _currentUser.Id,
                    IdModuleTemplate = createAttestationAnswerDto.IdModuleTemplate,
                    IdQuestionTemplate = createAttestationAnswerDto.IdQuestionTemplate,
                    IdAnswerTemplate = new List<int>(createAttestationAnswerDto.IdAnswerTemplates),
                    AnswerText = ""
                };
            }


            _attestationAnswerRepository.Create(attestationAnswer);

            return null;
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
