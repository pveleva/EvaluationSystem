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
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IFormModule;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class ModuleService : IModuleService, IExceptionService
    {
        private IMapper _mapper;
        private IAnswerRepository _answerRepository;
        private IQuestionRepository _questionRepository;
        private IModuleRepository _moduleRepository;
        private IModuleQuestionRepository _moduleQuestionRepository;
        private IFormModuleRepository _formModuleRepository;

        public ModuleService(IMapper mapper, IAnswerRepository answerRepository, IQuestionRepository questionRepository, IModuleRepository moduleRepository,
            IModuleQuestionRepository moduleQuestionRepository, IFormModuleRepository formModuleRepository)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _moduleRepository = moduleRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
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
        public GetModulesDto Create(GetModulesDto moduleDto)
        {
            ModuleTemplate module = _mapper.Map<ModuleTemplate>(moduleDto);
            int moduleId = _moduleRepository.Create(module);
            module.Id = moduleId;

            List<QuestionTemplate> questions = new List<QuestionTemplate>();

            foreach (var questionDto in moduleDto.QuestionsDtos)
            {
                QuestionTemplate question = _mapper.Map<QuestionTemplate>(questionDto);
                question.IsReusable = false;
                question.DateOfCreation = DateTime.UtcNow;
                int questionId = _questionRepository.Create(question);
                question.Id = questionId;

                questions.Add(question);

                _moduleQuestionRepository.Create(new ModuleQuestion()
                {
                    IdModule = moduleId,
                    IdQuestion = questionId,
                    Position = questionDto.Position
                });

                List<AnswerTemplate> answers = _mapper.Map<List<AnswerTemplate>>(question.AnswerText);
                foreach (var answer in answers)
                {
                    answer.IdQuestion = questionId;
                    _answerRepository.Create(answer);
                }
            }

            _formModuleRepository.Create(new FormModule()
            {
                IdForm = moduleDto.IdForm,
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
            //NOT FINISHED
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
