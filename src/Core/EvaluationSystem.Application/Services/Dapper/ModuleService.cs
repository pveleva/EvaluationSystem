using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class ModuleService : IModuleService
    {
        private IMapper _mapper;
        private IModuleRepository _moduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleService(IMapper mapper, IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _unitOfWork = unitOfWork;
        }
        public List<GetModulesDto> GetAll()
        {
            using (_unitOfWork)
            {
                List<GetModuleQuestionAnswerDto> modelsRepo = _moduleRepository.GetAll();

                List<GetModulesDto> modules = modelsRepo.GroupBy(x => new { x.IdModule, x.NameModule })
                    .Select(q => new GetModulesDto()
                    {
                        Id = q.Key.IdModule,
                        Name = q.Key.NameModule,
                        QuestionsDtos = new List<QuestionDto>()
                    }).Distinct().ToList();

                List<QuestionDto> questions = modelsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion })
                    .Select(q => new QuestionDto()
                    {
                        IdModule = q.Key.IdModule,
                        Id = q.Key.IdQuestion,
                        Name = q.Key.NameQuestion,
                        AnswerText = new List<AnswerDto>()
                    }).ToList();

                List<AnswerDto> answers = modelsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.AnswerText })
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

                _unitOfWork.Commit();
                return modules;
            }
        }

        public GetModulesDto GetById(int id)
        {
            using (_unitOfWork)
            {
                List<GetModuleQuestionAnswerDto> modelsRepo = _moduleRepository.GetByIDFromRepo(id);

                List<GetModulesDto> module = modelsRepo.GroupBy(x => new { x.IdModule, x.NameModule })
                    .Select(q => new GetModulesDto()
                    {
                        Id = q.Key.IdModule,
                        Name = q.Key.NameModule,
                        QuestionsDtos = new List<QuestionDto>()
                    }).ToList();

                List<QuestionDto> questions = modelsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion })
                    .Select(q => new QuestionDto()
                    {
                        IdModule = q.Key.IdModule,
                        Id = q.Key.IdQuestion,
                        Name = q.Key.NameQuestion,
                        AnswerText = new List<AnswerDto>()
                    }).ToList();

                List<AnswerDto> answers = modelsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.AnswerText })
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

                module.FirstOrDefault().QuestionsDtos = questions;

                _unitOfWork.Commit();
                return module.FirstOrDefault();
            }
        }
        public ExposeModuleDto Create(CreateUpdateModuleDto moduleDto)
        {
            using (_unitOfWork)
            {
                ModuleTemplate module = _mapper.Map<ModuleTemplate>(moduleDto);
                int moduleId = _moduleRepository.Create(module);
                module.Id = moduleId;

                _unitOfWork.Commit();
                return _mapper.Map<ExposeModuleDto>(module);
            }
        }
        public ExposeModuleDto Update(int id, CreateUpdateModuleDto moduleDto)
        {
            using (_unitOfWork)
            {
                ModuleTemplate moduleToUpdate = _moduleRepository.GetByID(id);

                _mapper.Map(moduleDto, moduleToUpdate);

                moduleToUpdate.Id = id;
                _moduleRepository.Update(moduleToUpdate);

                _unitOfWork.Commit();
                return _mapper.Map<ExposeModuleDto>(moduleToUpdate);
            }
        }

        public void DeleteFromRepo(int id)
        {
            using (_unitOfWork)
            {
                _moduleRepository.DeleteFromRepo(id);
                _unitOfWork.Commit();
            }
        }
    }
}
