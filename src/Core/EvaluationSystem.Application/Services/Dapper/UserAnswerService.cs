using System;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Interfaces.IUserAnswer;
using EvaluationSystem.Application.Interfaces.IAttestation;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Interfaces.IAttestationForm;
using EvaluationSystem.Application.Interfaces.IAttestationQuestion;
using EvaluationSystem.Application.Interfaces.IAttestationParticipant;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class UserAnswerService : IUserAnswerService, IExceptionService
    {
        private readonly IAttestationQuestionService _questionService;
        private readonly IAttestationFormService _formService;
        private readonly IUserRepository _userRepository;
        private readonly IAttestationRepository _attestationRepository;
        private readonly IAttestationService _attestationService;
        private readonly IAttestationParticipantRepository _attestationParticipantRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IUser _currentUser;
        public UserAnswerService(IAttestationQuestionService questionService, IAttestationFormService formService, IUserRepository userRepository, IAttestationRepository attestationRepository,
            IAttestationService attestationService, IAttestationParticipantRepository attestationParticipantRepository, IUserAnswerRepository userAnswerRepository, IUser currentUser)
        {
            _questionService = questionService;
            _formService = formService;
            _userRepository = userRepository;
            _attestationRepository = attestationRepository;
            _attestationService = attestationService;
            _attestationParticipantRepository = attestationParticipantRepository;
            _userAnswerRepository = userAnswerRepository;
            _currentUser = currentUser;
        }
        public CreateGetFormDto Get(int idAttestation, string email)
        {
            var attestation = _attestationService.GetAll().Where(a => a.IdAttestation == idAttestation).FirstOrDefault();
            var user = _userRepository.GetList().Where(u => u.Email == email).FirstOrDefault();
            var attestationAnswer = _userAnswerRepository.GetList().Where(aa => aa.IdAttestation == idAttestation && aa.IdUserParticipant == user.Id).ToList();
            var form = _formService.GetAll().Where(f => f.Name == attestation.FormName).FirstOrDefault();

            foreach (var ans in attestationAnswer)
            {
                var module = form.ModulesDtos.Where(m => m.Id == ans.IdAttestationModule).FirstOrDefault();
                var question = module.QuestionsDtos.Where(q => q.Id == ans.IdAttestationQuestion).FirstOrDefault();

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
                        if (a.Id == ans.IdAttestationAnswer)
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

            var attestationAnswer = new UserAnswer()
            {
                IdAttestation = createAttestationAnswerDto.IdAttestation,
                IdUserParticipant = _currentUser.Id
            };

            foreach (var answer in createAttestationAnswerDto.AttestationAnswerBodyDtos)
            {
                var questionType = _questionService.GetById(answer.IdModuleTemplate, answer.IdQuestionTemplate).Type;

                if (questionType == Domain.Entities.Type.TextField)
                {
                    attestationAnswer.IdAttestationModule = answer.IdModuleTemplate;
                    attestationAnswer.IdAttestationQuestion = answer.IdQuestionTemplate;
                    attestationAnswer.IdAttestationAnswer = 0;
                    attestationAnswer.TextAnswer = answer.AnswerText;

                    _userAnswerRepository.Create(attestationAnswer);
                }
                else
                {
                    foreach (var ans in answer.IdAnswerTemplates)
                    {
                        attestationAnswer.IdAttestationModule = answer.IdModuleTemplate;
                        attestationAnswer.IdAttestationQuestion = answer.IdQuestionTemplate;
                        attestationAnswer.IdAttestationAnswer = ans;
                        attestationAnswer.TextAnswer = "";

                        _userAnswerRepository.Create(attestationAnswer);
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
