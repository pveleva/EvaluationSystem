using System;
using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Answers;
using System.Linq;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class FormService : IFormService
    {
        private IMapper _mapper;
        private IFormRepository _formRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FormService(IMapper mapper, IFormRepository formRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _formRepository = formRepository;
            _unitOfWork = unitOfWork;
        }

        public List<GetFormDto> GetAll()
        {
            using (_unitOfWork)
            {
                List<GetFormModuleQuestionAnswerDto> formsRepo = _formRepository.GetAll();

                List<GetFormDto> forms = formsRepo.GroupBy(x => new { x.IdForm, x.NameForm })
                    .Select(q => new GetFormDto()
                    {
                        Id = q.Key.IdForm,
                        Name = q.Key.NameForm,
                        ModulesDtos = new List<GetModulesDto>()
                    }).ToList();

                List<GetModulesDto> modules = formsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule })
                    .Select(q => new GetModulesDto()
                    {
                        IdForm = q.Key.IdForm,
                        Id = q.Key.IdModule,
                        Name = q.Key.NameModule,
                        QuestionsDtos = new List<QuestionDto>()
                    }).ToList();

                List<QuestionDto> questions = formsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion })
                    .Select(q => new QuestionDto()
                    {
                        IdModule = q.Key.IdModule,
                        Id = q.Key.IdQuestion,
                        Name = q.Key.NameQuestion,
                        AnswerText = new List<AnswerDto>()
                    }).ToList();

                List<AnswerDto> answers = formsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.AnswerText })
                    .Select(q => new AnswerDto()
                    {
                        IdQuestion = q.Key.IdQuestion,
                        Id = q.Key.IdAnswer,
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

                _unitOfWork.Commit();
                return forms;
            }
        }

        public GetFormDto GetById(int id)
        {
            using (_unitOfWork)
            {
                List<GetFormModuleQuestionAnswerDto> formsRepo = _formRepository.GetByIDFromRepo(id);

                List<GetFormDto> forms = formsRepo.GroupBy(x => new { x.IdForm, x.NameForm })
                    .Select(q => new GetFormDto()
                    {
                        Id = q.Key.IdForm,
                        Name = q.Key.NameForm,
                        ModulesDtos = new List<GetModulesDto>()
                    }).ToList();

                List<GetModulesDto> modules = formsRepo.GroupBy(x => new { x.IdForm, x.IdModule, x.NameModule })
                    .Select(q => new GetModulesDto()
                    {
                        IdForm = q.Key.IdForm,
                        Id = q.Key.IdModule,
                        Name = q.Key.NameModule,
                        QuestionsDtos = new List<QuestionDto>()
                    }).ToList();

                List<QuestionDto> questions = formsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion })
                    .Select(q => new QuestionDto()
                    {
                        IdModule = q.Key.IdModule,
                        Id = q.Key.IdQuestion,
                        Name = q.Key.NameQuestion,
                        AnswerText = new List<AnswerDto>()
                    }).ToList();

                List<AnswerDto> answers = formsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.AnswerText })
                    .Select(q => new AnswerDto()
                    {
                        IdQuestion = q.Key.IdQuestion,
                        Id = q.Key.IdAnswer,
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

                _unitOfWork.Commit();
                return forms.FirstOrDefault();
            }
        }

        public ExposeFormDto Create(CreateUpdateFormDto formDto)
        {
            using (_unitOfWork)
            {
                FormTemplate form = _mapper.Map<FormTemplate>(formDto);
                int formId = _formRepository.Create(form);
                form.Id = formId;

                _unitOfWork.Commit();
                return _mapper.Map<ExposeFormDto>(form);
            }
        }

        public ExposeFormDto Update(int id, CreateUpdateFormDto formDto)
        {
            using (_unitOfWork)
            {
                FormTemplate formToUpdate = _formRepository.GetByID(id);

                _mapper.Map(formDto, formToUpdate);

                formToUpdate.Id = id;
                _formRepository.Update(formToUpdate);

                _unitOfWork.Commit();
                return _mapper.Map<ExposeFormDto>(formToUpdate);
            }
        }

        public void DeleteFromRepo(int id)
        {
            using (_unitOfWork)
            {
                _formRepository.DeleteFromRepo(id);
                _unitOfWork.Commit();
            }
        }
    }
}
