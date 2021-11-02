using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private IMapper _mapper;
        private IQuestionRepository _questionRepository;

        public QuestionService(IMapper mapper, IQuestionRepository repository)
        {
            _mapper = mapper;
            _questionRepository = repository;
        }
        public List<QuestionDto> GetAllQuestions()
        {
            List<Question> questions = _questionRepository.GetAllQuestions();

            List<QuestionDto> questionDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionDtos;
        }
        public QuestionDto GetQuestionById(int id)
        {
            Question question = _questionRepository.GetQuestionById(id);

            QuestionDto questionDto = _mapper.Map<QuestionDto>(question);
            return questionDto;
        }

        public QuestionDto CreateQuestion(CreateQuestionDto questionDto)
        {
            Question question = _mapper.Map<Question>(questionDto);
            return _mapper.Map<QuestionDto>(questionDto);
        }

        public QuestionDto UpdateQuestion(UpdateQuestionDto question)
        {
            if (_questionRepository.GetQuestionById(question.Id) == null)
            {
                throw new Exception($"Question with {question.Id} does not exist!");
            }

            Question questionToUpdate = _mapper.Map<Question>(question);
            return _mapper.Map<QuestionDto>(question);
        }
        public void DeleteQuestion(int id)
        {
            _questionRepository.DeleteQuestion(id);
        }
    }
}
