using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        public AnswerService(IMapper mapper,IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }
        public AnswerDto CreateAnswer(int questionId, CreateUpdateAnswerDto answerDto)
        {
            Answer answer = _mapper.Map<Answer>(answerDto);
            answer.IdQuestion = questionId;
            _answerRepository.AddAnswerToDatabase(answer);
            return  _mapper.Map<AnswerDto>(answerDto);
        }

        public void DeleteAnswer(int questionId, int answerId)
        {
            _answerRepository.DeleteAnswer(questionId, answerId);
        }

        public IEnumerable<AnswerDto> GetAllAnswers(int questionId)
        {
            IEnumerable<Answer> answers = _answerRepository.GetAllAnswers(questionId);
            return _mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public AnswerDto GetAnswerById(int questionId, int answerId)
        {
            Answer answer = _answerRepository.GetAnswerById(questionId, answerId);
            return _mapper.Map<AnswerDto>(answer);
        }

        public AnswerDto UpdateAnswer(int questionId, int answerId, CreateUpdateAnswerDto answerDto)
        {
            Answer answer = _mapper.Map<Answer>(answerDto);
            answer.Id = answerId;
            answer.IdQuestion = questionId;
            _answerRepository.UpdateAnswer(answer);
            return _mapper.Map<AnswerDto>(answerDto);
        }
    }
}
