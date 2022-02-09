using System;
using System.Linq;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Application.Interfaces.IUserAnswer;
using EvaluationSystem.Application.Interfaces.IAttestation;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Interfaces.IAttestationForm;
using EvaluationSystem.Application.Interfaces.IAttestationQuestion;
using EvaluationSystem.Application.Interfaces.IAttestationParticipant;
using System.Collections.Generic;
using EvaluationSystem.Application.Answers;

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
            User user = null;
            if (email == "null")
            {
                user = _userRepository.GetList().Where(u => u.Email == _currentUser.Email).FirstOrDefault();
            }
            else
            {
                user = _userRepository.GetList().Where(u => u.Email == email).FirstOrDefault();
            }
            var attestationAnswer = _userAnswerRepository.GetList().Where(aa => aa.IdAttestation == idAttestation && aa.IdUserParticipant == user.Id).ToList();
            var form = _formService.GetById(attestation.IdAttestationForm);

            foreach (var ans in attestationAnswer)
            {
                var module = form.ModulesDtos.Where(m => m.Id == ans.IdAttestationModule).FirstOrDefault();
                var question = module.QuestionsDtos.Where(q => q.Id == ans.IdAttestationQuestion).FirstOrDefault();

                if (question.Type == Domain.Entities.Type.TextField)
                {
                    question.AnswerText = new List<AnswerDto>() { new AnswerDto { IdQuestion = question.Id, AnswerText = ans.TextAnswer, IsAnswered = 1 } };
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
        public void Update(UpdateAttestationAnswerDto updateAttestationAnswerDto)
        {
            ThrowExceptionWhenEntityDoNotExist(updateAttestationAnswerDto.IdAttestation, _attestationRepository);

            var attestationAnswer = new UserAnswer()
            {
                IdAttestation = updateAttestationAnswerDto.IdAttestation,
                IdUserParticipant = _currentUser.Id
            };

            var questionType = _questionService.GetById(updateAttestationAnswerDto.IdAttestationModule, updateAttestationAnswerDto.IdAttestationQuestion).Type;

            if (questionType == Domain.Entities.Type.TextField)
            {
                _userAnswerRepository.DeleteAttestationAnswer(updateAttestationAnswerDto.IdAttestation, _currentUser.Id, updateAttestationAnswerDto.IdAttestationModule,
                updateAttestationAnswerDto.IdAttestationQuestion, updateAttestationAnswerDto.IdAttestationAnswer);

                if (updateAttestationAnswerDto.AnswerText != "" && updateAttestationAnswerDto.AnswerText != null)
                {
                    attestationAnswer.IdAttestationModule = updateAttestationAnswerDto.IdAttestationModule;
                    attestationAnswer.IdAttestationQuestion = updateAttestationAnswerDto.IdAttestationQuestion;
                    attestationAnswer.IdAttestationAnswer = 0;
                    attestationAnswer.TextAnswer = updateAttestationAnswerDto.AnswerText;

                    _userAnswerRepository.Create(attestationAnswer);
                }
            }
            else if (questionType == Domain.Entities.Type.CheckBox)
            {
                if (_userAnswerRepository.IsAttestationAnswerExist(updateAttestationAnswerDto.IdAttestation, _currentUser.Id, updateAttestationAnswerDto.IdAttestationModule,
                    updateAttestationAnswerDto.IdAttestationQuestion, updateAttestationAnswerDto.IdAttestationAnswer) != null)
                {
                    _userAnswerRepository.DeleteAttestationAnswer(updateAttestationAnswerDto.IdAttestation, _currentUser.Id, updateAttestationAnswerDto.IdAttestationModule,
                    updateAttestationAnswerDto.IdAttestationQuestion, updateAttestationAnswerDto.IdAttestationAnswer);
                }
                else
                {
                    attestationAnswer.IdAttestationModule = updateAttestationAnswerDto.IdAttestationModule;
                    attestationAnswer.IdAttestationQuestion = updateAttestationAnswerDto.IdAttestationQuestion;
                    attestationAnswer.IdAttestationAnswer = updateAttestationAnswerDto.IdAttestationAnswer;
                    attestationAnswer.TextAnswer = "";

                    _userAnswerRepository.Create(attestationAnswer);
                }
            }
            else
            {
                _userAnswerRepository.DeleteAllAttestationAnswer(updateAttestationAnswerDto.IdAttestation, _currentUser.Id, updateAttestationAnswerDto.IdAttestationModule,
                updateAttestationAnswerDto.IdAttestationQuestion);

                attestationAnswer.IdAttestationModule = updateAttestationAnswerDto.IdAttestationModule;
                attestationAnswer.IdAttestationQuestion = updateAttestationAnswerDto.IdAttestationQuestion;
                attestationAnswer.IdAttestationAnswer = updateAttestationAnswerDto.IdAttestationAnswer;
                attestationAnswer.TextAnswer = "";

                _userAnswerRepository.Create(attestationAnswer);
            }
        }
        public void Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            ThrowExceptionWhenEntityDoNotExist(createAttestationAnswerDto.IdAttestation, _attestationRepository);
            _attestationParticipantRepository.UpdateFromRepo(createAttestationAnswerDto.IdAttestation, _currentUser.Id);
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
