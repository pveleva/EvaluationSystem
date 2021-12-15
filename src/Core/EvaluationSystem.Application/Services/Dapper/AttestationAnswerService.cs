using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestation;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;
using EvaluationSystem.Application.Interfaces.IAttestationParticipant;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationAnswerService : IAttestationAnswerService, IExceptionService
    {
        private readonly IQuestionService _questionService;
        private readonly IFormService _formService;
        private readonly IUserRepository _userRepository;
        private readonly IAttestationRepository _attestationRepository;
        private readonly IAttestationService _attestationService;
        private readonly IAttestationParticipantRepository _attestationParticipantRepository;
        private readonly IAttestationAnswerRepository _attestationAnswerRepository;
        private readonly IUser _currentUser;
        public AttestationAnswerService(IQuestionService questionService, IFormService formService, IUserRepository userRepository, IAttestationRepository attestationRepository,
            IAttestationService attestationService, IAttestationParticipantRepository attestationParticipantRepository, IAttestationAnswerRepository attestationAnswerRepository, IUser currentUser)
        {
            _questionService = questionService;
            _formService = formService;
            _userRepository = userRepository;
            _attestationRepository = attestationRepository;
            _attestationService = attestationService;
            _attestationParticipantRepository = attestationParticipantRepository;
            _attestationAnswerRepository = attestationAnswerRepository;
            _currentUser = currentUser;
        }
        public CreateGetFormDto Get(int idAttestation, string email)
        {
            var attestation = _attestationService.GetAll().Where(a => a.IdAttestation == idAttestation).FirstOrDefault();
            var user = _userRepository.GetList().Where(u => u.Email == email).FirstOrDefault();
            var attestationAnswer = _attestationAnswerRepository.GetList().Where(aa => aa.IdAttestation == idAttestation && aa.IdUserParticipant == user.Id).ToList();
            var form = _formService.GetAll().Where(f => f.Name == attestation.FormName).FirstOrDefault();

            foreach (var ans in attestationAnswer)
            {
                var module = form.ModulesDtos.Where(m => m.Id == ans.IdModuleTemplate).FirstOrDefault();
                var question = module.QuestionsDtos.Where(q => q.Id == ans.IdQuestionTemplate).FirstOrDefault();

                if (question.Type == Domain.Entities.Type.TextField)
                {
                    question.AnswerText = new List<AnswerDto>()
                    { new AnswerDto()
                    { AnswerText = ans.TextAnswer, IsAnswered = 1 } };
                }
                else
                {
                    foreach (var a in question.AnswerText)
                    {
                        if (a.Id == ans.IdAnswerTemplate)
                        {
                            a.IsAnswered = 1;
                        }
                    }
                }
            }
            return form;
        }
        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            ThrowExceptionWhenEntityDoNotExist(createAttestationAnswerDto.IdAttestation, _attestationRepository);

            var attestationAnswer = new AttestationAnswer()
            {
                IdAttestation = createAttestationAnswerDto.IdAttestation,
                IdUserParticipant = _currentUser.Id
            };

            foreach (var answer in createAttestationAnswerDto.AttestationAnswerBodyDtos)
            {
                var questionType = _questionService.GetById(answer.IdModuleTemplate, answer.IdQuestionTemplate).Type;

                if (questionType == Domain.Entities.Type.TextField)
                {
                    attestationAnswer.IdModuleTemplate = answer.IdModuleTemplate;
                    attestationAnswer.IdQuestionTemplate = answer.IdQuestionTemplate;
                    attestationAnswer.IdAnswerTemplate = 0;
                    attestationAnswer.TextAnswer = answer.AnswerText;

                    _attestationAnswerRepository.Create(attestationAnswer);
                }
                else
                {
                    foreach (var ans in answer.IdAnswerTemplates)
                    {
                        attestationAnswer.IdModuleTemplate = answer.IdModuleTemplate;
                        attestationAnswer.IdQuestionTemplate = answer.IdQuestionTemplate;
                        attestationAnswer.IdAnswerTemplate = ans;
                        attestationAnswer.TextAnswer = "";

                        _attestationAnswerRepository.Create(attestationAnswer);
                    }
                };
            }

            _attestationParticipantRepository.UpdateFromRepo(createAttestationAnswerDto.IdAttestation, _currentUser.Id);

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
