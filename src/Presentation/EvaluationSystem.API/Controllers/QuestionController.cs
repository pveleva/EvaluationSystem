using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces.IQuestion;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/question")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _service;
        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        [HttpGet()]
        public List<QuestionDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{questionId}")]
        public QuestionDto GetById(int questionId)
        {
            return _service.GetById(questionId);
        }

        [HttpPost]
        public QuestionDto Create(QuestionDto questionDto)
        {
            return _service.Create(questionDto);
        }

        [HttpPut("{questionId}")]
        public QuestionDto Update(int questionId, UpdateQuestionDto questionDto)
        {
            return _service.Update(questionId, questionDto);
        }

        [HttpDelete("{questionId}")]
        public IActionResult Delete(int questionId)
        {
            _service.Delete(questionId);
            return StatusCode(204);
        }
    }
}