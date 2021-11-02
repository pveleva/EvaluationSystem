using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public QuestionDto GetQuestionById(int id)
        {
            return _service.GetQuestionById(id);
        }

        [HttpPut()]
        public QuestionDto CreateQuestion(CreateQuestionDto questionDto)
        {
            return _service.CreateQuestion(questionDto);
        }

        [HttpPatch("{questionId}")]
        public QuestionDto UpdateQuestion(UpdateQuestionDto questionDto)
        {
            return _service.UpdateQuestion(questionDto);
        }

        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(int id)
        {
            _service.DeleteQuestion(id);
            return StatusCode(204);
        }
    }
}
