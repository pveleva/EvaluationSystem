using System;
using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using System.Linq;

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
        public List<CreateUpdateModuleDto> GetAll()
        {
            using (_unitOfWork)
            {
                List<GetModulesDto> modelsRepo = _moduleRepository.GetAll();

                List<GetModulesDto> models = modelsRepo.GroupBy(x => new { x.Id, x.Name })
                    .Select(q => new GetModulesDto()
                    {
                        Id = q.Key.Id,
                        Name = q.Key.Name,
                        QuestionsDtos = new List<QuestionDto>()
                    }).ToList();

                List<QuestionDto> questions = modelsRepo.GroupBy(x => new { x.Name, x.IdQuestion })
                    .Select(q => new QuestionDto()
                    {
                        IdQuestion = q.Key.IdQuestion,
                        Name = q.Key.Name,
                        AnswerText = new List<AnswerDto>()
                    }).ToList();

                List<AnswerDto> answers = modelsRepo.GroupBy(x => new { x.Name, x.IdQuestion, x.IdAnswer, x.AnswerText })
                    .Select(q => new AnswerDto()
                    {
                        IdQuestion = q.Key.IdQuestion,
                        Id = q.Key.IdAnswer,
                        AnswerText = q.Key.AnswerText
                    }).ToList();

                foreach (var question in questions)
                {
                    question.AnswerText = answers.Where(a => a.IdQuestion == question.IdQuestion);
                }

                _unitOfWork.Commit();
                return questions;
            }
        }

        public CreateUpdateModuleDto GetById(int id)
        {
            throw new NotImplementedException();
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
