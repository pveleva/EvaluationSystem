using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IModule;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class ModuleService : IModuleService
    {
        private IMapper _mapper;
        private IModuleRepository _moduleRepository;

        public ModuleService(IMapper mapper, IModuleRepository moduleRepository)
        {
            _mapper = mapper;
            _moduleRepository = moduleRepository;
        }
        public List<GetModulesDto> GetAll()
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

            return modules;
        }

        public GetModulesDto GetById(int id)
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

            return module.FirstOrDefault();
        }
        public ExposeModuleDto Create(CreateUpdateModuleDto moduleDto)
        {
            ModuleTemplate module = _mapper.Map<ModuleTemplate>(moduleDto);
            int moduleId = _moduleRepository.Create(module);
            module.Id = moduleId;

            return _mapper.Map<ExposeModuleDto>(module);
        }
        public ExposeModuleDto Update(int id, CreateUpdateModuleDto moduleDto)
        {
            ModuleTemplate moduleToUpdate = _moduleRepository.GetByID(id);

            _mapper.Map(moduleDto, moduleToUpdate);

            moduleToUpdate.Id = id;
            _moduleRepository.Update(moduleToUpdate);

            return _mapper.Map<ExposeModuleDto>(moduleToUpdate);
        }

        public void DeleteFromRepo(int id)
        {
            _moduleRepository.DeleteFromRepo(id);
        }
    }
}
