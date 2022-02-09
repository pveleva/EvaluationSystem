using System;
using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IQuestion;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AnswerService : IAnswerService, IExceptionService
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private IQuestionRepository _questionRepository;
        private readonly IMemoryCache _memoryCache;
        public AnswerService(IMapper mapper, IAnswerRepository answerRepository, IQuestionRepository questionRepository, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
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

            AnswerTemplate answer = _mapper.Map<AnswerTemplate>(answerDto);
            answer.IdQuestion = questionId;
            int answerId = _answerRepository.Create(answer);
            answer.Id = answerId;

            return _mapper.Map<AnswerDto>(answer);
        }

        public AnswerDto Update(int questionId, int answerId, CreateUpdateAnswerDto answerDto)
        {
            AnswerTemplate answerToUpdate = _answerRepository.GetByID(answerId);
            answerToUpdate.IsDefault = answerDto.IsDefault == answerToUpdate.IsDefault ? answerToUpdate.IsDefault : answerDto.IsDefault;
            answerToUpdate.Position = answerDto.Position == answerToUpdate.Position ? answerToUpdate.Position : answerDto.Position;
            answerToUpdate.AnswerText = answerDto.AnswerText == answerToUpdate.AnswerText ? answerToUpdate.AnswerText : answerDto.AnswerText;

            _answerRepository.Update(answerToUpdate);

            _memoryCache.Remove($"answer {answerId}");
            return _mapper.Map<AnswerDto>(answerToUpdate);
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
