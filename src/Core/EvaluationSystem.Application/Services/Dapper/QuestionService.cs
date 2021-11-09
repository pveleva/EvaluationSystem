using AutoMapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Questions.Dapper;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class QuestionService : IQuestionService
    {
        private IMapper _mapper;
        private IQuestionRepository _questionRepository;
        private IAnswerRepository _answerRepository;
        public QuestionService(IMapper mapper, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public List<QuestionDto> GetAllQuestions()
        {
            List<GetQuestionsDto> questionsRepo = _questionRepository.GetAllQuestions();

            List<QuestionDto> questions = questionsRepo.GroupBy(x => new { x.Name, x.IdQuestion })
                .Select(q => new QuestionDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    Name = q.Key.Name,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = questionsRepo.GroupBy(x => new { x.Name, x.IdQuestion, x.IdAnswer, x.AnswerText })
                .Select(q => new AnswerDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    IdAnswer = q.Key.IdAnswer,
                    AnswerText = q.Key.AnswerText
                }).ToList();

            foreach (var question in questions)
            {
                question.AnswerText = answers.Where(a => a.IdQuestion == question.IdQuestion);
            }

            return questions;
        }

        public QuestionDto GetQuestionById(int id)
        {
            List<GetQuestionsDto> questionRepo = _questionRepository.GetQuestionById(id);

            List<QuestionDto> question = questionRepo.GroupBy(x => new { x.Name, x.IdQuestion })
                .Select(q => new QuestionDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    Name = q.Key.Name,
                    AnswerText = new List<AnswerDto>()
                }).ToList();

            List<AnswerDto> answers = questionRepo.GroupBy(x => new { x.Name, x.IdQuestion, x.IdAnswer, x.AnswerText })
                .Select(q => new AnswerDto()
                {
                    IdQuestion = q.Key.IdQuestion,
                    IdAnswer = q.Key.IdAnswer,
                    AnswerText = q.Key.AnswerText
                }).ToList();

            question.FirstOrDefault().AnswerText = answers;
            return question.FirstOrDefault();
        }

        public QuestionDto CreateQuestion(CreateQuestionDto questionDto)
        {
            Question question = _mapper.Map<Question>(questionDto);
            int questionId = _questionRepository.AddQuestionToDatabase(question);

            ICollection<Answer> answers = _mapper.Map<ICollection<Answer>>(questionDto.Answers);

            foreach (var answer in answers)
            {
                answer.IdQuestion = questionId;
                _answerRepository.AddAnswerToDatabase(answer);
            }

            return GetQuestionById(questionId);
        }

        public QuestionDto UpdateQuestion(int id, UpdateQuestionDto questionDto)
        {
            Question questionToUpdate = _mapper.Map<Question>(questionDto);
            questionToUpdate.Id = id;
            _questionRepository.UpdateQuestion(questionToUpdate);

            return GetQuestionById(id);
        }

        public void DeleteQuestion(int id)
        {
            _questionRepository.DeleteQuestion(id);
        }
    }
}
