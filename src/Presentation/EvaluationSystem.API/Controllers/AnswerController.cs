using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Answers.Dapper;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/question/{questionId}/answer")]
    [ApiController]
    public class AnswerController : AuthorizeAdminController
    {
        private IAnswerService _service;
        public AnswerController(IAnswerService service)
        {
            _service = service;
        }

        [HttpGet()]
        public IEnumerable<AnswerDto> GetAll(int questionId)
        {
            return _service.GetAll(questionId);
        }

        [HttpGet("{answerId}")]
        public AnswerDto GetById(int questionId, int answerId)
        {
            return _service.GetByID(questionId, answerId);
        }

        [HttpPost()]
        public AnswerDto Create([FromRoute] int questionId, [FromBody] AnswerDto answerDto)
        {
            return _service.Create(questionId, answerDto);
        }

        [HttpPut("{answerId}")]
        public AnswerDto Update(int questionId, int answerId, [FromBody] CreateUpdateAnswerDto answerDto)
        {
            return _service.Update(questionId, answerId, answerDto);
        }

        [HttpDelete("{answerId}")]
        public IActionResult Delete(int answerId)
        {
            _service.Delete(answerId);
            return StatusCode(204);
        }
    }
}
