using System;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class QuestionService : IQuestionService, IExceptionService
    {
        private IMapper _mapper;
        private IAnswerService _answerService;
        private IQuestionRepository _questionRepository;
        private IModuleRepository _moduleRepository;
        private IModuleQuestionRepository _moduleQuestionRepository;
        public QuestionService(IMapper mapper, IAnswerService answerService, IQuestionRepository questionRepository, IModuleRepository moduleRepository,
            IModuleQuestionRepository moduleQuestionRepository)
        {
            _mapper = mapper;
            _answerService = answerService;
            _questionRepository = questionRepository;
            _moduleRepository = moduleRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
        }

        public List<QuestionDto> GetAll(int moduleId)
        {
            ThrowExceptionWhenEntityDoNotExist(moduleId, _moduleRepository);

            List<GetQuestionsDto> questionsRepo = _questionRepository.GetAll(moduleId);

            List<QuestionDto> questions = questionsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = questionsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
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

            var moduleQuestions = questions.Where(q => q.IdModule == moduleId).ToList();

            return moduleQuestions;
        }

        public QuestionDto GetById(int moduleId, int questionId)
        {
            ThrowExceptionWhenEntityDoNotExist(moduleId, _moduleRepository);
            ThrowExceptionWhenEntityDoNotExist(questionId, _questionRepository);

            List<GetQuestionsDto> questionRepo = _questionRepository.GetByIDFromRepo(moduleId, questionId);

            List<QuestionDto> question = questionRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = questionRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
                .Select(q => new AnswerDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    Id = q.Key.IdAnswer,
                    IsDefault = q.Key.IsDefault,
                    AnswerText = q.Key.AnswerText
                }).ToList();

            var result = question.FirstOrDefault().AnswerText = answers;

            return question.FirstOrDefault();
        }

        public QuestionDto Create(int moduleId, QuestionDto questionDto)
        {
            QuestionTemplate question = _mapper.Map<QuestionTemplate>(questionDto);
            question.IsReusable = false;
            int questionId = _questionRepository.Create(question);

            if (questionDto.Type == Domain.Entities.Type.Numeric)
            {
                foreach (var answer in questionDto.AnswerText)
                {
                    int answerParsed;
                    bool isNumeric = int.TryParse(answer.AnswerText, out answerParsed);
                    if (!isNumeric)
                    {
                        throw new ArgumentException("Answer type is not numeric!");
                    }
                }
            }

            foreach (var answer in questionDto.AnswerText)
            {
                _answerService.Create(questionId, answer);
            }

            _moduleQuestionRepository.Create(new ModuleQuestion()
            {
                IdModule = moduleId,
                IdQuestion = questionId,
                Position = questionDto.Position != 0 ? questionDto.Position : 1
            });

            return GetById(moduleId, questionId);
        }

        public QuestionDto Update(int moduleId, int questionId, UpdateQuestionDto questionDto)
        {
            QuestionTemplate questionToUpdate = _mapper.Map<QuestionTemplate>(GetById(moduleId, questionId));

            _mapper.Map(questionDto, questionToUpdate);

            questionToUpdate.Id = questionId;
            _questionRepository.Update(questionToUpdate);

            return GetById(moduleId, questionId);
        }

        public List<QuestionDto> GetAll()
        {

            List<GetQuestionsDto> questionsRepo = _questionRepository.GetAll();

            List<QuestionDto> questions = questionsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = questionsRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
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

            return questions;
        }

        public QuestionDto GetById(int id)
        {
            ThrowExceptionWhenEntityDoNotExist(id, _questionRepository);

            List<GetQuestionsDto> questionRepo = _questionRepository.GetByIDFromRepo(id);

            List<QuestionDto> question = questionRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = questionRepo.GroupBy(x => new { x.IdQuestion, x.IdAnswer, x.IsDefault, x.AnswerText })
                .Select(q => new AnswerDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    Id = q.Key.IdAnswer,
                    IsDefault = q.Key.IsDefault,
                    AnswerText = q.Key.AnswerText
                }).ToList();

            var result = question.FirstOrDefault().AnswerText = answers;

            return question.FirstOrDefault();
        }

        public QuestionDto Create(QuestionDto questionDto)
        {
            QuestionTemplate question = _mapper.Map<QuestionTemplate>(questionDto);
            question.IsReusable = true;
            int questionId = _questionRepository.Create(question);

            if (questionDto.Type == Domain.Entities.Type.Numeric)
            {
                foreach (var answer in questionDto.AnswerText)
                {
                    int answerParsed;
                    bool isNumeric = int.TryParse(answer.AnswerText, out answerParsed);
                    if (!isNumeric)
                    {
                        throw new ArgumentException("Answer type is not numeric!");
                    }
                }
            }

            foreach (var answer in questionDto.AnswerText)
            {
                answer.IdQuestion = questionId;
                _answerService.Create(questionId, answer);
            }

            return GetById(questionId);
        }

        public QuestionDto Update(int id, UpdateQuestionDto questionDto)
        {
            QuestionTemplate questionToUpdate = _questionRepository.GetByID(id);

            _mapper.Map(questionDto, questionToUpdate);

            questionToUpdate.Id = id;
            _questionRepository.Update(questionToUpdate);

            return GetById(id);
        }
        public void Delete(int id)
        {
            _questionRepository.DeleteFromRepo(id);
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
