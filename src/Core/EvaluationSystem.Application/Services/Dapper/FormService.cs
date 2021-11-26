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
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IFormModule;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class FormService : IFormService, IExceptionService
    {
        private IMapper _mapper;
        private IAnswerRepository _answerRepository;
        private IQuestionRepository _questionRepository;
        private IModuleRepository _moduleRepository;
        private IModuleQuestionRepository _moduleQuestionRepository;
        private IFormModuleRepository _formModuleRepository;
        private IFormRepository _formRepository;

        public FormService(IMapper mapper, IAnswerRepository answerRepository, IQuestionRepository questionRepository, IModuleRepository moduleRepository,
            IModuleQuestionRepository moduleQuestionRepository, IFormModuleRepository formModuleRepository, IFormRepository formRepository)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _moduleRepository = moduleRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
            _formModuleRepository = formModuleRepository;
            _formRepository = formRepository;
        }

        public List<CreateGetFormDto> GetAll()
        {
            List<GetFormModuleQuestionAnswerDto> formsRepo = _formRepository.GetAll();

            List<CreateGetFormDto> forms = formsRepo.GroupBy(x => new { x.IdForm, x.NameForm, x.IdModule })
                .Select(q => new CreateGetFormDto()
                {
                    Id = q.Key.IdForm,
                    Name = q.Key.NameForm,
                    IdModule = q.Key.IdModule,
                    ModulesDtos = new List<GetModulesDto>()
                }).ToList();

            List<GetModulesDto> modules = formsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule, x.ModulePosition })
                .Select(q => new GetModulesDto()
                {
                    IdForm = q.Key.IdForm,
                    Id = q.Key.IdModule,
                    Name = q.Key.NameModule,
                    Position = q.Key.ModulePosition,
                    QuestionsDtos = new List<QuestionDto>()
                }).ToList();

            List<QuestionDto> questions = formsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = formsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
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

            foreach (var form in forms)
            {
                form.ModulesDtos = modules.Where(m => m.IdForm == form.Id).ToList();
            }

            return forms;
        }

        public CreateGetFormDto GetById(int id)
        {
            ThrowExceptionWhenEntityDoNotExist(id, _formRepository);

            List<GetFormModuleQuestionAnswerDto> formsRepo = _formRepository.GetByIDFromRepo(id);

            List<CreateGetFormDto> forms = formsRepo.GroupBy(x => new { x.IdForm, x.NameForm, x.IdModule })
                .Select(q => new CreateGetFormDto()
                {
                    Id = q.Key.IdForm,
                    Name = q.Key.NameForm,
                    IdModule = q.Key.IdModule,
                    ModulesDtos = new List<GetModulesDto>()
                }).ToList();

            List<GetModulesDto> modules = formsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule, x.ModulePosition })
                .Select(q => new GetModulesDto()
                {
                    IdForm = q.Key.IdForm,
                    Id = q.Key.IdModule,
                    Name = q.Key.NameModule,
                    Position = q.Key.ModulePosition,
                    QuestionsDtos = new List<QuestionDto>()
                }).ToList();

            List<QuestionDto> questions = formsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = formsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
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

            forms.FirstOrDefault().ModulesDtos = modules;

            return forms.FirstOrDefault();
        }

        public CreateGetFormDto Create(CreateGetFormDto formDto)
        {
            FormTemplate form = _mapper.Map<FormTemplate>(formDto);
            int formId = _formRepository.Create(form);
            form.Id = formId;

            List<ModuleTemplate> modules = new List<ModuleTemplate>();

            foreach (var moduleDto in formDto.ModulesDtos)
            {

                ModuleTemplate module = _mapper.Map<ModuleTemplate>(moduleDto);
                int moduleId = _moduleRepository.Create(module);
                module.Id = moduleId;

                modules.Add(module);

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
                    IdForm = formId,
                    IdModule = moduleId,
                    Position = moduleDto.Position
                });
            }

            return GetById(formId);
        }

        public ExposeFormDto Update(int id, UpdateFormDto formDto)
        {
            FormTemplate formToUpdate = _formRepository.GetByID(id);

            _mapper.Map(formDto, formToUpdate);

            formToUpdate.Id = id;
            _formRepository.Update(formToUpdate);

            return _mapper.Map<ExposeFormDto>(formToUpdate);
        }

        public void DeleteFromRepo(int id)
        {
            _formRepository.DeleteFromRepo(id);
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
