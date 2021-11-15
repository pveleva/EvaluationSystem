using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AnswerService(IMapper mapper, IAnswerRepository answerRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
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

        public AnswerDto GetById(int questionId, int answerId)
        {
            using (_unitOfWork)
            {
                AnswerTemplate answer = _answerRepository.GetByID(answerId);

                _unitOfWork.Commit();
                return _mapper.Map<AnswerDto>(answer);
            }
        }
        public AnswerDto CreateAnswer(int questionId, CreateUpdateAnswerDto answerDto)
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

        public AnswerDto UpdateAnswer(int questionId, int answerId, CreateUpdateAnswerDto answerDto)
        {
            using (_unitOfWork)
            {
                AnswerTemplate answer = _mapper.Map<AnswerTemplate>(answerDto);
                answer.Id = answerId;
                answer.IdQuestion = questionId;
                _answerRepository.Update(answer);

                _unitOfWork.Commit();
                return _mapper.Map<AnswerDto>(answer);
            }
        }

        public void DeleteAnswer(int answerId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.Commit();
                _answerRepository.Delete(answerId);
            }
        }
    }
}
