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
                question.AnswerText = answers.Where(a => a.IdQuestion == question.Id);
            }

            foreach (var module in modules)
            {

                module.QuestionsDtos = questions.Where(q => q.IdModule == module.Id).ToList();
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
                question.AnswerText = answers.Where(a => a.IdQuestion == question.Id);
            }

            modules.FirstOrDefault().QuestionsDtos = questions;

            return modules.FirstOrDefault();
        }
        public GetModulesDto Create(int formId, GetModulesDto moduleDto)
        {
            ModuleTemplate module = _mapper.Map<ModuleTemplate>(moduleDto);
            int moduleId = _moduleRepository.Create(module);
            module.Id = moduleId;

            foreach (var questionDto in moduleDto.QuestionsDtos)
            {
                _questionService.Create(moduleId,questionDto);
            }

            _formModuleRepository.Create(new FormModule()
            {
                IdForm = formId,
                IdModule = moduleId,
                Position = moduleDto.Position
            });

            return GetById(moduleId);
        }
        public ExposeModuleDto Update(int id, UpdateModuleDto moduleDto)
        {
            ModuleTemplate moduleToUpdate = _moduleRepository.GetByID(id);

            _mapper.Map(moduleDto, moduleToUpdate);

            moduleToUpdate.Id = id;
            _moduleRepository.Update(moduleToUpdate);

            return _mapper.Map<ExposeModuleDto>(moduleToUpdate);
        }

        public void DeleteFromRepo(int id)
        {
            var moduleDto = GetById(id);
            moduleDto.QuestionsDtos = _questionService.GetAll(id);

            foreach (var question in moduleDto.QuestionsDtos)
            {
                _questionService.Delete(question.Id);
            }

            _moduleRepository.DeleteFromRepo(id);
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
