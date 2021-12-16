using System;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;
using EvaluationSystem.Application.Interfaces.IAttestationQuestion;
using EvaluationSystem.Application.Interfaces.IAttestationModuleQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationQuestionService : IAttestationQuestionService
    {
        private IMapper _mapper;
        private IAttestationAnswerService _answerService;
        private IAttestationQuestionRepository _questionRepository;
        private IAttestationModuleQuestionRepository _moduleQuestionRepository;
        public AttestationQuestionService(IMapper mapper, IAttestationAnswerService answerService, IAttestationQuestionRepository questionRepository,
            IAttestationModuleQuestionRepository moduleQuestionRepository)
        {
            _mapper = mapper;
            _answerService = answerService;
            _questionRepository = questionRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
        }

        public List<QuestionDto> GetAll(int moduleId)
        {
            List<GetQuestionsDto> questionsRepo = _questionRepository.GetAll(moduleId);

            List<QuestionDto> questions = questionsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition, x.DateOfCreation })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    DateOfCreation = q.Key.DateOfCreation,
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
                    question.AnswerText = answers.Where(a => a.IdQuestion == question.Id).ToList();
                }
            }

            var moduleQuestions = questions.Where(q => q.IdModule == moduleId).ToList();

            return moduleQuestions;
        }

        public QuestionDto GetById(int moduleId, int questionId)
        {
            List<GetQuestionsDto> questionRepo = _questionRepository.GetByIDFromRepo(moduleId, questionId);

            List<QuestionDto> question = questionRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition, x.DateOfCreation })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    DateOfCreation = q.Key.DateOfCreation,
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

            question.FirstOrDefault().AnswerText = answers;

            return question.FirstOrDefault();
        }

        public QuestionDto Create(int moduleId, QuestionDto questionDto)
        {
            AttestationQuestion question = _mapper.Map<AttestationQuestion>(questionDto);
            question.IsReusable = false;
            question.DateOfCreation = DateTime.UtcNow;
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

            _moduleQuestionRepository.Create(new AttestationModuleQuestion()
            {
                IdModule = moduleId,
                IdQuestion = questionId,
                Position = questionDto.Position != 0 ? questionDto.Position : 1
            });

            return GetById(moduleId, questionId);
        }

        public List<QuestionDto> GetAll()
        {
            List<GetQuestionsDto> questionsRepo = _questionRepository.GetAll();

            List<QuestionDto> questions = questionsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition, x.DateOfCreation })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    DateOfCreation = q.Key.DateOfCreation,
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
                    question.AnswerText = answers.Where(a => a.IdQuestion == question.Id).ToList();
                }
            }

            return questions;
        }

        public QuestionDto GetById(int id)
        {
            List<GetQuestionsDto> questionRepo = _questionRepository.GetByIDFromRepo(id);

            List<QuestionDto> question = questionRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition, x.DateOfCreation })
                .Select(q => new QuestionDto()
                {
                    IdModule = q.Key.IdModule,
                    Id = q.Key.IdQuestion,
                    Name = q.Key.NameQuestion,
                    Type = q.Key.Type,
                    Position = q.Key.QuestionPosition,
                    DateOfCreation = q.Key.DateOfCreation,
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

            question.FirstOrDefault().AnswerText = answers;

            return question.FirstOrDefault();
        }

        public QuestionDto Create(QuestionDto questionDto)
        {
            AttestationQuestion question = _mapper.Map<AttestationQuestion>(questionDto);
            question.IsReusable = true;
            question.DateOfCreation = DateTime.UtcNow;
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
    }
}