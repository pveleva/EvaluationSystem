﻿using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class QuestionService : IQuestionService
    {
        private IMapper _mapper;
        private IQuestionRepository _questionRepository;
        private IAnswerRepository _answerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionService(IMapper mapper, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
        }

        public List<QuestionDto> GetAll()
        {
            using (_unitOfWork)
            {
                List<GetQuestionsDto> questionsRepo = _questionRepository.GetAll();

                List<QuestionDto> questions = questionsRepo.GroupBy(x => new { x.Name, x.IdQuestion })
                    .Select(q => new QuestionDto()
                    {
                        IdQuestion = q.Key.IdQuestion,
                        Name = q.Key.Name,
                        AnswerText = new List<AnswerDto>()
                    }).ToList();

                List<AnswerDto> answers = questionsRepo.GroupBy(x => new { x.Name, x.IdQuestion, x.IdAnswer, x.AnswerText })
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

        public QuestionDto GetQuestionById(int id)
        {
            using (_unitOfWork)
            {
                List<GetQuestionsDto> questionRepo = _questionRepository.GetByIDFromRepo(id);

                List<QuestionDto> question = questionRepo.GroupBy(x => new { x.Name, x.IdQuestion })
                    .Select(q => new QuestionDto()
                    {
                        IdQuestion = q.Key.IdQuestion,
                        Name = q.Key.Name,
                        AnswerText = new List<AnswerDto>()
                    }).ToList();

                List<AnswerDto> answers = questionRepo.GroupBy(x => new { x.Name, x.IdQuestion, x.IdAnswer, x.AnswerText })
                    .Select(q => new AnswerDto()
                    {
                        IdQuestion = q.Key.IdQuestion,
                        Id = q.Key.IdAnswer,
                        AnswerText = q.Key.AnswerText
                    }).ToList();

                question.FirstOrDefault().AnswerText = answers;

                _unitOfWork.Commit();
                return question.FirstOrDefault();
            }
        }

        public QuestionDto CreateQuestion(CreateQuestionDto questionDto)
        {
            using (_unitOfWork)
            {
                QuestionTemplate question = _mapper.Map<QuestionTemplate>(questionDto);
                int questionId = (int)_questionRepository.Create(question);

                ICollection<AnswerTemplate> answers = _mapper.Map<ICollection<AnswerTemplate>>(questionDto.Answers);

                foreach (var answer in answers)
                {
                    answer.IdQuestion = questionId;
                    _answerRepository.Create(answer);
                }

                _unitOfWork.Commit();
                return GetQuestionById(questionId);
            }
        }

        public QuestionDto UpdateQuestion(int id, UpdateQuestionDto questionDto)
        {
            using (_unitOfWork)
            {
                QuestionTemplate questionToUpdate = _questionRepository.GetByID(id);

                _mapper.Map(questionDto, questionToUpdate);

                questionToUpdate.Id = id;
                _questionRepository.Update(questionToUpdate);

                _unitOfWork.Commit();
                return GetQuestionById(id);
            }
        }

        public void DeleteQuestion(int id)
        {
            using (_unitOfWork)
            {
                _questionRepository.DeleteFromRepo(id);
                _unitOfWork.Commit();
            }
        }
    }
}
