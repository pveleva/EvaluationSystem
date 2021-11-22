using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        public AnswerService(IMapper mapper, IAnswerRepository answerRepository, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public List<AnswerDto> GetAll(int questionId)
        {
            using (_unitOfWork)
            {
                List<AnswerTemplate> answers = _answerRepository.GetAll(questionId);

                _unitOfWork.Commit();
                return _mapper.Map<List<AnswerDto>>(answers);
            }
        }

        public AnswerDto GetByID(int questionId, int answerId)
        {
            using (_unitOfWork)
            {
                var answerCache = _memoryCache.Get($"answer {answerId}");
                if (answerCache != null)
                {
                    return (AnswerDto)answerCache;
                }

                AnswerTemplate answer = _answerRepository.GetByID(answerId);
                AnswerDto answerDto = _mapper.Map<AnswerDto>(answer);
                _memoryCache.Set($"answer {answerId}", answerDto);

                _unitOfWork.Commit();
                return answerDto;
            }
        }
        public AnswerDto Create(int questionId, CreateUpdateAnswerDto answerDto)
        {
            using (_unitOfWork)
            {
                AnswerTemplate answer = _mapper.Map<AnswerTemplate>(answerDto);
                answer.IdQuestion = questionId;
                int answerId = _answerRepository.Create(answer);
                answer.Id = answerId;

                _unitOfWork.Commit();
                return _mapper.Map<AnswerDto>(answer);
            }
        }

        public AnswerDto Update(int questionId, int answerId, CreateUpdateAnswerDto answerDto)
        {
            using (_unitOfWork)
            {
                AnswerTemplate answer = _mapper.Map<AnswerTemplate>(answerDto);
                answer.Id = answerId;
                answer.IdQuestion = questionId;
                _answerRepository.Update(answer);

                _memoryCache.Remove($"answer {answerId}");
                _unitOfWork.Commit();
                return _mapper.Map<AnswerDto>(answer);
            }
        }

        public void Delete(int answerId)
        {
            using (_unitOfWork)
            {   
                _memoryCache.Remove($"answer {answerId}");
                _unitOfWork.Commit();
                _answerRepository.Delete(answerId);
            }
        }
    }
}
