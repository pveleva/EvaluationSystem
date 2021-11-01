using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Services
{
    public class AnswerService : IAnswerService
    {
        private IMapper mapper;
        private IAnswerRepository repository;
        public AnswerService(IMapper mapper, IAnswerRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public AnswerDto GetAnswerById(int questionId, int answerId)
        {
            Answer answer = repository.GetAnswerById(questionId, answerId);

            AnswerDto answerDto = mapper.Map<AnswerDto>(answer);
            return answerDto;
        }
        public Answer CreateAnswer(CreateAnswerDto answerDto)
        {
            Answer answer = mapper.Map<Answer>(answerDto);
            return answer;
        }

        public string UpdateAnswer(UpdateAnswerDto answer)
        {
            Answer answerToUpdate = mapper.Map<Answer>(answer);
            if (repository.UpdateAnswer(answerToUpdate))
            {
                return "Successfully updated!";
            }
            return $"Answer with id: {answer.Id} does not exist!";
        }

        public string DeleteAnswer(int id)
        {
            if (repository.DeleteAnswer(id))
            {
                return "Successfully deleted!";
            }
            return $"Answer with id: {id} does not exist!";
        }
    }
}
