using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Questions.Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _service;
        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        [HttpGet()]
        public List<QuestionDto> GetAllQuestions()
        {
            return _service.GetAllQuestions();
        }

        [HttpGet("{questionId}")]
        public QuestionDto GetQuestionById(int questionId)
        {
            return _service.GetQuestionById(questionId);
        }

        [HttpPost()]
        public QuestionDto CreateQuestion(CreateQuestionDto questionDto)
        {
            return _service.CreateQuestion(questionDto);
        }

        [HttpPut("{questionId}")]
        public QuestionDto UpdateQuestion(int questionId, UpdateQuestionDto questionDto)
        {
            return _service.UpdateQuestion(questionId, questionDto);
        }

        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(int questionId)
        {
            _service.DeleteQuestion(questionId);
            return StatusCode(204);
        }
    }
}
