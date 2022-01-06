using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces.IQuestion;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/module/{moduleId}/question")]
    [ApiController]
    public class QuestionController : AuthorizeControllerBase
    {
        private IQuestionService _service;
        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        [HttpGet()]
        public List<QuestionDto> GetAll(int moduleId)
        {
            return _service.GetAll(moduleId);
        }

        [HttpGet("{questionId}")]
        public QuestionDto GetById(int moduleId, int questionId)
        {
            return _service.GetById(moduleId, questionId);
        }

        [HttpPost()]
        public QuestionDto CreateCustom(int moduleId, QuestionDto questionDto)
        {
            return _service.Create(moduleId, questionDto);
        }

        [HttpPut("{questionId}")]
        public QuestionDto Update(int moduleId, int questionId, UpdateCustomQuestionDto questionDto)
        {
            return _service.Update(moduleId, questionId, questionDto);
        }

        [HttpDelete("{questionId}")]
        public IActionResult Delete(int questionId)
        {
            _service.Delete(questionId);
            return StatusCode(204);
        }
    }
}
