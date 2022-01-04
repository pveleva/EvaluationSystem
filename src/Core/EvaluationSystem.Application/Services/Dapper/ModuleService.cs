using System;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IFormModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class ModuleService : IModuleService, IExceptionService
    {
        private IMapper _mapper;
        private IQuestionService _questionService;
        private IModuleRepository _moduleRepository;
        private IFormModuleRepository _formModuleRepository;

        public ModuleService(IMapper mapper, IQuestionService questionService, IModuleRepository moduleRepository, IFormModuleRepository formModuleRepository)
        {
            _mapper = mapper;
            _questionService = questionService;
            _moduleRepository = moduleRepository;
            _formModuleRepository = formModuleRepository;
        }
        public List<GetModulesDto> GetAll()
        {
            List<GetFormModuleQuestionAnswerDto> modelsRepo = _moduleRepository.GetAll();

            List<GetModulesDto> modules = modelsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule, x.ModulePosition })
                .Select(q => new GetModulesDto()
                {
                    IdForm = q.Key.IdForm,
                    Id = q.Key.IdModule,
                    Name = q.Key.NameModule,
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
            ThrowExceptionWhenEntityDoNotExist(id, _moduleRepository);

            List<GetFormModuleQuestionAnswerDto> modelsRepo = _moduleRepository.GetByIDFromRepo(id);

            List<GetModulesDto> modules = modelsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule, x.ModulePosition })
                .Select(q => new GetModulesDto()
                {
                    IdForm = q.Key.IdForm,
                    Id = q.Key.IdModule,
                    Name = q.Key.NameModule,
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
        public GetModulesDto Create(int formId, GetModulesDto moduleDto)
        {
            ModuleTemplate module = _mapper.Map<ModuleTemplate>(moduleDto);
            int moduleId = _moduleRepository.Create(module);
            module.Id = moduleId;

            GetModulesDto moduleToReturn = _mapper.Map<GetModulesDto>(module);
            moduleToReturn.QuestionsDtos = new List<QuestionDto>();

            foreach (var questionDto in moduleDto.QuestionsDtos)
            {
                var a = _questionService.Create(moduleId, questionDto);
                moduleToReturn.QuestionsDtos.Add(a);
            }

            _formModuleRepository.Create(new FormModule()
            {
                IdForm = formId,
                IdModule = moduleId,
                Position = moduleDto.Position != 0 ? moduleDto.Position : 1
            });

            return moduleToReturn;
        }
        public GetModulesDto Update(int id, UpdateModuleDto moduleDto)
        {
            GetModulesDto moduleToUpdate = GetById(id);
            moduleToUpdate.Name = moduleDto.Name == null ? moduleToUpdate.Name : moduleDto.Name;

            _moduleRepository.Update(_mapper.Map<ModuleTemplate>(moduleToUpdate));

            if (moduleDto.Position != 0)
            {
                _formModuleRepository.UpdateFromRepo(moduleToUpdate.IdForm, id, moduleDto.Position);
            }

            return _mapper.Map<GetModulesDto>(moduleToUpdate);
        }

        public void DeleteFromRepo(int id)
        {
            _moduleRepository.Delete(id);
        }

        public void ThrowExceptionWhenEntityDoNotExist<T>(int id, IGenericRepository<T> repository)
        {
            var entity = repository.GetByID(id);
            var entityName = typeof(T).Name.Remove(typeof(T).Name.Length - 8);
            if (entity == null)
            {
                throw new NullReferenceException($"{entityName} with ID:{id} doesn't exist!");
            }
        }
    }
}
