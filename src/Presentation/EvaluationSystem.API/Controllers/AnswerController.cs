using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Answers.Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/question/{questionId}/answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private IAnswerService _service;
        public AnswerController(IAnswerService service)
        {
            _service = service;
        }

        [HttpGet()]
        public IEnumerable<AnswerDto> GetAllAnswer(int questionId)
        {
            return _service.GetAllAnswers(questionId);
        }

        [HttpGet("{answerId}")]
        public AnswerDto GetAnswerById(int questionId, int answerId)
        {
            return _service.GetAnswerById(questionId, answerId);
        }

        [HttpPost()]
        public AnswerDto CreateAnswer(int questionId, [FromBody] CreateUpdateAnswerDto answerDto)
        {
            return _service.CreateAnswer(questionId, answerDto);
        }

        [HttpPut("{answerId}")]
        public AnswerDto UpdateAnswer(int questionId, int answerId, [FromBody] CreateUpdateAnswerDto answerDto)
        {
            return _service.UpdateAnswer(questionId, answerId, answerDto);
        }

        [HttpDelete("{answerId}")]
        public IActionResult DeleteAnswer(int questionId, int answerId)
        {
            _service.DeleteAnswer(questionId, answerId);
            return StatusCode(204);
        }
    }
}
