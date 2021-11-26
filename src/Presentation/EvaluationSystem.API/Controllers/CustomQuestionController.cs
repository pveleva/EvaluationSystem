using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Interfaces.IQuestion;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/module/{moduleId}/question")]
    [ApiController]
    public class CustomQuestionController : ControllerBase
    {
        private IQuestionService _service;
        public CustomQuestionController(IQuestionService service)
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
        public QuestionDto CreateCustom(int moduleId, CreateModuleQuestionDto questionDto)
        {
            return _service.Create(moduleId, questionDto);
        }

        [HttpPut("{questionId}")]
        public QuestionDto Update(int moduleId, int questionId, UpdateQuestionDto questionDto)
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
