using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestation;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationAnswerService : IAttestationAnswerService, IExceptionService
    {
        private readonly IQuestionService _questionService;
        private readonly IFormService _formService;
        private readonly IAttestationRepository _attestationRepository;
        private readonly IAttestationService _attestationService;
        private readonly IAttestationAnswerRepository _attestationAnswerRepository;
        private readonly IUser _currentUser;
        public AttestationAnswerService(IQuestionService questionService, IFormService formService, IAttestationRepository attestationRepository,
            IAttestationService attestationService, IAttestationAnswerRepository attestationAnswerRepository, IUser currentUser)
        {
            _questionService = questionService;
            _formService = formService;
            _attestationRepository = attestationRepository;
            _attestationService = attestationService;
            _attestationAnswerRepository = attestationAnswerRepository;
            _currentUser = currentUser;
        }

        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            ThrowExceptionWhenEntityDoNotExist(createAttestationAnswerDto.IdAttestation, _attestationRepository);

            var attestationAnswer = new AttestationAnswer()
            {
                IdAttestation = createAttestationAnswerDto.IdAttestation,
                IdUserParticipant = _currentUser.Id
            };

            var formName = _attestationService.GetAll().Where(a => a.IdAttestation == createAttestationAnswerDto.IdAttestation).FirstOrDefault().FormName;
            var formId = _formService.GetAll().Where(f => f.Name == formName).FirstOrDefault().Id;


            foreach (var answer in createAttestationAnswerDto.AttestationAnswerBodyDtos)
            {
                var questionType = _questionService.GetById(answer.IdModuleTemplate, answer.IdQuestionTemplate).Type;

                if (questionType == Domain.Entities.Type.TextField)
                {
                    attestationAnswer = new AttestationAnswer()
                    {
                        IdModuleTemplate = answer.IdModuleTemplate,
                        IdQuestionTemplate = answer.IdQuestionTemplate,
                        IdAnswerTemplate = new List<int>(),
                        TextAnswer = answer.AnswerText
                    };

                    _attestationAnswerRepository.Create(attestationAnswer);
                }
                else
                {
                    attestationAnswer = new AttestationAnswer()
                    {
                        IdModuleTemplate = answer.IdModuleTemplate,
                        IdQuestionTemplate = answer.IdQuestionTemplate,
                        IdAnswerTemplate = new List<int>(answer.IdAnswerTemplates),
                        TextAnswer = ""
                    };

                    _attestationAnswerRepository.Create(attestationAnswer);
                }
            }

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
