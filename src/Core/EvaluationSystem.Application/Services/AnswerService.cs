using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        public AnswerService(IMapper mapper, IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }
        public List<AnswerDto> GetAllAnswers(int questionId)
        {
            List<Answer> answer = _answerRepository.GetAllAnswers(questionId);

            return _mapper.Map<List<AnswerDto>>(answer);
        }
        public AnswerDto GetAnswerById(int questionId, int answerId)
        {
            Answer answer = _answerRepository.GetAnswerById(questionId, answerId);

            return _mapper.Map<AnswerDto>(answer);
        }
        public AnswerDto CreateAnswer(CreateAnswerDto answerDto)
        {
            if (_questionRepository.GetQuestionById(answerDto.QuestionId) == null)
            {
                throw new Exception($"Question with {answerDto.QuestionId} does not exist!");
            }

            Answer answer = _mapper.Map<Answer>(answerDto);
            _answerRepository.AddAnswerToDatabase(answer);
            return _mapper.Map<AnswerDto>(answerDto);
        }

        public AnswerDto UpdateAnswer(UpdateAnswerDto answer)
        {
            Question question = _questionRepository.GetQuestionById(answer.QuestionId);

            if (question == null)
            {
                throw new Exception($"Question with {answer.QuestionId} does not exist!");

                if (_answerRepository.GetAnswerById(answer.QuestionId, answer.Id) == null)
                {
                    throw new Exception($"Answer with {answer.Id} does not exist!");
                }
            }

            //if (question.Type!=answer)
            //{
                    //TODO: 
            //}

            Answer answerToUpdate = _mapper.Map<Answer>(answer); //това никъде не се използва? нещо май не е, както трябва?
            return _mapper.Map<AnswerDto>(answer);
        }

        public void DeleteAnswer(int questionId, int answerId)
        {
            if (_questionRepository.GetQuestionById(questionId) == null)
            {
                throw new Exception($"Question with {questionId} does not exist!");
            }
            _answerRepository.DeleteAnswer(questionId, answerId);
        }
    }
}
