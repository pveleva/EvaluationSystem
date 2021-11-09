using AutoMapper;
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
            List<Question> questions = _questionRepository.GetAllQuestions();

            List<GetQuestionsDto> questionsDto = _mapper.Map<List<GetQuestionsDto>>(questions);

            List<QuestionDto> result = questionsDto.GroupBy(x => x.Name)
                .Select(q => new QuestionDto()
                {
                    Name = q.Key,
                    AnswerText = questions
                .Where(y => y.Name == q.Key)
                .Select(y => y.AnswerText).ToList()
                }).ToList();

            return result;
        }

        public QuestionDto GetQuestionById(int id)
        {
            List<Question> questions = _questionRepository.GetQuestionById(id);

            List<GetQuestionsDto> questionsDto = _mapper.Map<List<GetQuestionsDto>>(questions);

            QuestionDto result = questionsDto.GroupBy(q => q.Name)
                .Select(q => new QuestionDto()
                {
                    Name = q.Key,
                    AnswerText = q
                .Where(y => y.Name == q.Key)
                .Select(y => y.AnswerText).ToList()
                }).FirstOrDefault();

            return result;
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
