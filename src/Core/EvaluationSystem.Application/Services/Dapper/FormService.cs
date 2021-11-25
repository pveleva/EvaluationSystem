using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IForm;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class FormService : IFormService
    {
        private IMapper _mapper;
        private IFormRepository _formRepository;

        public FormService(IMapper mapper, IFormRepository formRepository)
        {
            _mapper = mapper;
            _formRepository = formRepository;
        }

        public List<GetFormDto> GetAll()
        {
            List<GetFormModuleQuestionAnswerDto> formsRepo = _formRepository.GetAll();

            List<GetFormDto> forms = formsRepo.GroupBy(x => new { x.IdForm, x.NameForm, x.IdModule })
                .Select(q => new GetFormDto()
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

        public GetFormDto GetById(int id)
        {
            List<GetFormModuleQuestionAnswerDto> formsRepo = _formRepository.GetByIDFromRepo(id);

            List<GetFormDto> forms = formsRepo.GroupBy(x => new { x.IdForm, x.NameForm, x.IdModule })
                .Select(q => new GetFormDto()
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

        public ExposeFormDto Create(CreateUpdateFormDto formDto)
        {
            FormTemplate form = _mapper.Map<FormTemplate>(formDto);
            int formId = _formRepository.Create(form);
            form.Id = formId;

            return _mapper.Map<ExposeFormDto>(form);
        }

        public ExposeFormDto Update(int id, CreateUpdateFormDto formDto)
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
    }
}
