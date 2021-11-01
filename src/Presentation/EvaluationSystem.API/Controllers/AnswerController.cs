using EvaluationSystem.Application.Answers;
using EvaluationSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EvaluationSystem.API.Controllers
{
    public class AnswerController : ControllerBase
    {
        private IAnswerService service;
        public AnswerController(IAnswerService service)
        {
            this.service = service;
        }

        [HttpGet("AnswerById")]
        public AnswerDto GetAnswerById(int questionId, int answerId)
        {
            return service.GetAnswerById(questionId, answerId);
        }

        [HttpPut("CreateAnswer")]
        public Answer CreateAnswer(CreateAnswerDto answerDto)
        {
            return service.CreateAnswer(answerDto);
        }

        [HttpPatch("UpdateAnswer")]
        public string UpdateAnswer(UpdateAnswerDto answerDto)
        {
            return service.UpdateAnswer(answerDto);
        }

        [HttpDelete("DeleteAnswer")]
        public string DeleteAnswer(int id)
        {
            return service.DeleteAnswer(id);
        }
    }
}
