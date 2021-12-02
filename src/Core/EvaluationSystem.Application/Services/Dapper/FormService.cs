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
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class FormService : IFormService, IExceptionService
    {
        private IMapper _mapper;
        private IModuleService _moduleService;
        private IFormRepository _formRepository;

        public FormService(IMapper mapper, IModuleService moduleService, IFormRepository formRepository)
        {
            _mapper = mapper;
            _moduleService = moduleService;
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
                if (answers.Any(a => a.IdQuestion == question.Id && a.Id != 0))
                {
                    question.AnswerText = answers.Where(a => a.IdQuestion == question.Id);
                }
            }

            foreach (var module in modules)
            {
                if (questions.Any(q => q.IdModule == module.Id && q.Id != 0))
                {
                    module.QuestionsDtos = questions.Where(q => q.IdModule == module.Id).ToList();
                }
            }

            foreach (var form in forms)
            {
                if (modules.Any(m => m.IdForm == form.Id && m.Id != 0))
                {
                    form.ModulesDtos = modules.Where(m => m.IdForm == form.Id).ToList();
                }
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
                if (answers.Any(a => a.IdQuestion == question.Id && a.Id != 0))
                {
                    question.AnswerText = answers.Where(a => a.IdQuestion == question.Id);
                }
            }

            foreach (var module in modules)
            {
                if (questions.Any(q => q.IdModule == module.Id && q.Id != 0))
                {
                    module.QuestionsDtos = questions.Where(q => q.IdModule == module.Id).ToList();
                }
            }

            if (forms.FirstOrDefault().ModulesDtos != null)
            {
                forms.FirstOrDefault().ModulesDtos = modules;
            }

            return forms.FirstOrDefault();
        }

        public CreateGetFormDto Create(CreateGetFormDto formDto)
        {
            FormTemplate form = _mapper.Map<FormTemplate>(formDto);
            int formId = _formRepository.Create(form);
            form.Id = formId;

            foreach (var moduleDto in formDto.ModulesDtos)
            {
                _moduleService.Create(formId, moduleDto);
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
            var form = GetById(id);

            foreach (var module in form.ModulesDtos)
            {
                if (module.Id != 0)
                {
                    _moduleService.DeleteFromRepo(module.Id);
                }
            }

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
