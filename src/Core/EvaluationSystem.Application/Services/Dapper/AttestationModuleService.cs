using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IAttestationModule;
using EvaluationSystem.Application.Interfaces.IAttestationQuestion;
using EvaluationSystem.Application.Interfaces.IAttestationFormModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationModuleService : IAttestationModuleService
    {
        private IMapper _mapper;
        private IAttestationQuestionService _questionService;
        private IAttestationModuleRepository _moduleRepository;
        private IAttestationFormModuleRepository _formModuleRepository;

        public AttestationModuleService(IMapper mapper, IAttestationQuestionService questionService, IAttestationModuleRepository moduleRepository,
            IAttestationFormModuleRepository formModuleRepository)
        {
            _mapper = mapper;
            _questionService = questionService;
            _moduleRepository = moduleRepository;
            _formModuleRepository = formModuleRepository;
        }
        public List<GetModulesDto> GetAll()
        {
            List<GetFormModuleQuestionAnswerDto> modelsRepo = _moduleRepository.GetAll();

            List<GetModulesDto> modules = modelsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule, x.Description, x.ModulePosition })
                .Select(q => new GetModulesDto()
                {
                    IdForm = q.Key.IdForm,
                    Id = q.Key.IdModule,
                    Name = q.Key.NameModule,
                    Description = q.Key.Description,
                    Position = q.Key.ModulePosition,
                    QuestionsDtos = new List<QuestionDto>()
                }).Distinct().ToList();

            List<QuestionDto> questions = modelsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = modelsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
                .Select(q => new AnswerDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    Id = q.Key.IdAnswer,
                    IsDefault = q.Key.IsDefault,
                    AnswerText = q.Key.AnswerText
                }).ToList();

            foreach (var question in questions)
            {
                if (answers.Any(a => a.IdQuestion == question.Id && a.Id != 0))
                {
                    question.AnswerText = answers.Where(a => a.IdQuestion == question.Id).ToList();
                }
            }

            foreach (var module in modules)
            {
                if (questions.Any(q => q.IdModule == module.Id && q.Id != 0))
                {
                    module.QuestionsDtos = questions.Where(q => q.IdModule == module.Id).ToList();
                }
            }

            return modules;
        }

        public GetModulesDto GetById(int id)
        {
            List<GetFormModuleQuestionAnswerDto> modelsRepo = _moduleRepository.GetByIDFromRepo(id);

            List<GetModulesDto> modules = modelsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule, x.Description, x.ModulePosition })
                .Select(q => new GetModulesDto()
                {
                    IdForm = q.Key.IdForm,
                    Id = q.Key.IdModule,
                    Name = q.Key.NameModule,
                    Description = q.Key.Description,
                    Position = q.Key.ModulePosition,
                    QuestionsDtos = new List<QuestionDto>()
                }).Distinct().ToList();

            List<QuestionDto> questions = modelsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = modelsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
                .Select(q => new AnswerDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    Id = q.Key.IdAnswer,
                    IsDefault = q.Key.IsDefault,
                    AnswerText = q.Key.AnswerText
                }).ToList();

            foreach (var question in questions)
            {
                if (answers.Any(a => a.IdQuestion == question.Id && a.Id != 0))
                {
                    question.AnswerText = answers.Where(a => a.IdQuestion == question.Id).ToList();
                }
            }

            modules.FirstOrDefault().QuestionsDtos = questions;

            return modules.FirstOrDefault();
        }
        public void Create(int formId, GetModulesDto moduleDto)
        {
            AttestationModule module = _mapper.Map<AttestationModule>(moduleDto);
            module.Id = 0;
            int moduleId = _moduleRepository.Create(module);

            foreach (var questionDto in moduleDto.QuestionsDtos)
            {
                _questionService.Create(moduleId, questionDto);
            }

            _formModuleRepository.Create(new AttestationFormModule()
            {
                IdForm = formId,
                IdModule = moduleId,
                Position = moduleDto.Position != 0 ? moduleDto.Position : 1
            });
        }
        public void DeleteFromRepo(int id)
        {
            var moduleDto = GetById(id);
            moduleDto.QuestionsDtos = _questionService.GetAll(id);

            foreach (var question in moduleDto.QuestionsDtos)
            {
                if (question.Id != 0)
                {
                    _questionService.Delete(question.Id);
                }
            }

            _moduleRepository.DeleteFromRepo(id);
        }

    }
}

