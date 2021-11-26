﻿using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;
using System;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class QuestionService : IQuestionService
    {
        private IMapper _mapper;
        private IAnswerRepository _answerRepository;
        private IQuestionRepository _questionRepository;
        private IModuleQuestionRepository _moduleQuestionRepository;
        public QuestionService(IMapper mapper, IAnswerRepository answerRepository, IQuestionRepository questionRepository, IModuleQuestionRepository moduleQuestionRepository)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
        }

        public List<QuestionDto> GetAll(int moduleId)
        {
            List<GetQuestionsDto> questionsRepo = _questionRepository.GetAll(moduleId);

            List<QuestionDto> allQuestions = questionsRepo.GroupBy(x => new { x.IdModule, x.IdQuestion, x.NameQuestion, x.Type, x.QuestionPosition })
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

            foreach (var question in allQuestions)
            {
                question.AnswerText = answers.Where(a => a.IdQuestion == question.Id);
            }

            var moduleQuestions = allQuestions.Where(q => q.IdModule == moduleId).ToList();

            return moduleQuestions;
        }

        public QuestionDto GetById(int moduleId, int questionId)
        {
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

        public QuestionDto Create(int moduleId, CreateModuleQuestionDto questionDto)
        {
            QuestionTemplate question = _mapper.Map<QuestionTemplate>(questionDto);
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

            ICollection<AnswerTemplate> answers = _mapper.Map<ICollection<AnswerTemplate>>(questionDto.AnswerText);

            foreach (var answer in answers)
            {
                answer.IdQuestion = questionId;
                _answerRepository.Create(answer);
            }

            _moduleQuestionRepository.Create(new ModuleQuestion()
            {
                IdModule = questionDto.IdModule,
                IdQuestion = questionId,
                Position = questionDto.Position
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

        public void Delete(int id)
        {
            _questionRepository.DeleteFromRepo(id);
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
                question.AnswerText = answers.Where(a => a.IdQuestion == question.Id);
            }

            return questions;
        }

        public QuestionDto GetById(int id)
        {
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

        public QuestionDto Create(CreateQuestionDto questionDto)
        {
            QuestionTemplate question = _mapper.Map<QuestionTemplate>(questionDto);
            question.IsReusable = true;
            question.DateOfCreation = DateTime.UtcNow;
            int questionId = _questionRepository.Create(question);

            if (questionDto.Type==Domain.Entities.Type.Numeric)
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

            ICollection<AnswerTemplate> answers = _mapper.Map<ICollection<AnswerTemplate>>(questionDto.AnswerText);

            foreach (var answer in answers)
            {
                answer.IdQuestion = questionId;
                _answerRepository.Create(answer);
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
    }
}
