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
        private IMapper mapper;
        private IQuestionRepository repository;

        public QuestionService(IMapper mapper, IQuestionRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public QuestionDto GetQuestionById(int id)
        {
            Question question = repository.GetQuestionById(id);

            QuestionDto questionDto = mapper.Map<QuestionDto>(question);
            return questionDto;
        }

        public AnswerDto GetQuestionAnswer(int questionId, int answerId)
        {
            Answer answer = repository.GetQuestionAnswer(questionId, answerId);

            AnswerDto answerDto = mapper.Map<AnswerDto>(answer);
            return answerDto;
        }

        public Question CreateQuestion(CreateQuestionDto questionDto)
        {
            Question question = mapper.Map<Question>(questionDto);
            return question;
        }

        public string UpdateQuestion(UpdateQuestionDto question)
        {
            Question questionToUpdate = mapper.Map<Question>(question);
            if (repository.UpdateQuestion(questionToUpdate))
            {
                return "Successfully updated!";
            }
            return $"Question with id: {question.Id} does not exist!";
        }
        public string DeleteQuestion(int id)
        {
            if (repository.DeleteQuestion(id))
            {
                return "Question successfully deleted!";
            }
            return $"Question with id: {id} does not exist!";
        }

        public string DeleteQuestionAnswer(int questionId, int answerId)
        {
            return repository.DeleteQuestionAnswer(questionId, answerId);
        }
    }
}
