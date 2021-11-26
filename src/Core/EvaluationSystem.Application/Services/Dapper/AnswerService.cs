using System;
using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Answers.Dapper;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AnswerService : IAnswerService, IExceptionService
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMemoryCache _memoryCache;
        public AnswerService(IMapper mapper, IAnswerRepository answerRepository, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _memoryCache = memoryCache;
        }

        public List<AnswerDto> GetAll(int questionId)
        {
            List<AnswerTemplate> answers = _answerRepository.GetAll(questionId);

            return _mapper.Map<List<AnswerDto>>(answers);
        }

        public AnswerDto GetByID(int questionId, int answerId)
        {
            ThrowExceptionWhenEntityDoNotExist(answerId, _answerRepository);

            var answerCache = _memoryCache.Get($"answer {answerId}");
            if (answerCache != null)
            {
                return (AnswerDto)answerCache;
            }

            AnswerTemplate answer = _answerRepository.GetByID(answerId);
            AnswerDto answerDto = _mapper.Map<AnswerDto>(answer);
            _memoryCache.Set($"answer {answerId}", answerDto);

            return answerDto;
        }
        public AnswerDto Create(int questionId, CreateUpdateAnswerDto answerDto)
        {
            AnswerTemplate answer = _mapper.Map<AnswerTemplate>(answerDto);
            answer.IdQuestion = questionId;
            int answerId = _answerRepository.Create(answer);
            answer.Id = answerId;

            return _mapper.Map<AnswerDto>(answer);
        }

        public AnswerDto Update(int questionId, int answerId, CreateUpdateAnswerDto answerDto)
        {
            AnswerTemplate answer = _mapper.Map<AnswerTemplate>(answerDto);
            answer.Id = answerId;
            answer.IdQuestion = questionId;
            _answerRepository.Update(answer);

            _memoryCache.Remove($"answer {answerId}");
            return _mapper.Map<AnswerDto>(answer);
        }

        public void Delete(int answerId)
        {
            _memoryCache.Remove($"answer {answerId}");

            _answerRepository.Delete(answerId);
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
