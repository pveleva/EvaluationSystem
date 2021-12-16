using System;
using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;
using EvaluationSystem.Application.Interfaces.IAttestationQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AttestationAnswerService : IAttestationAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IAttestationAnswerRepository _answerRepository;
        private IAttestationQuestionRepository _questionRepository;
        public AttestationAnswerService(IMapper mapper, IAttestationAnswerRepository answerRepository, IAttestationQuestionRepository questionRepository)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }

        public List<AnswerDto> GetAll(int questionId)
        {
            List<AttestationAnswer> answers = _answerRepository.GetAll(questionId);

            return _mapper.Map<List<AnswerDto>>(answers);
        }

        public AnswerDto GetByID(int questionId, int answerId)
        {
            AttestationAnswer answer = _answerRepository.GetByID(answerId);
            AnswerDto answerDto = _mapper.Map<AnswerDto>(answer);

            return answerDto;
        }
        public AnswerDto Create(int questionId, AnswerDto answerDto)
        {
            if (_questionRepository.GetByID(questionId).Type == Domain.Entities.Type.Numeric)
            {
                int answerParsed;
                bool isNumeric = int.TryParse(answerDto.AnswerText, out answerParsed);
                if (!isNumeric)
                {
                    throw new ArgumentException("Answer type is not numeric!");
                }
            }

            AttestationAnswer answer = _mapper.Map<AttestationAnswer>(answerDto);
            answer.IdQuestion = questionId;
            int answerId = _answerRepository.Create(answer);
            answer.Id = answerId;

            return _mapper.Map<AnswerDto>(answer);
        }
        public void Delete(int answerId)
        {
            _answerRepository.Delete(answerId);
        }
    }
}
