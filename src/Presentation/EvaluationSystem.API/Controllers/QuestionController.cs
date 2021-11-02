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
    public class QuestionController : ControllerBase
    {
        private IQuestionService service;
        public QuestionController(IQuestionService service)
        {
            this.service = service;
        }

        [HttpGet("QuestionById")]
        public QuestionDto GetQuestionById(int id)
        {
            return service.GetQuestionById(id);
        }

        [HttpGet("QuestionAnswer")]
        public AnswerDto GetQuestionAnswer(int questionId, int answerId)
        {
            return service.GetQuestionAnswer(questionId, answerId);
        }

        [HttpPut("CreateQuestion")]
        public Question CreateQuestion(CreateQuestionDto questionDto)
        {
            return service.CreateQuestion(questionDto);
        }

        [HttpPatch("UpdateQuestion")]
        public string UpdateQuestion(UpdateQuestionDto questionDto)
        {
            return service.UpdateQuestion(questionDto);
        }

        [HttpDelete("DeleteQuestion")]
        public string DeleteQuestion(int id)
        {
            return service.DeleteQuestion(id);
        }

        [HttpDelete("DeleteQuestionAnswer")]
        public string DeleteQuestionAnswer(int questionId, int answerId)
        {
            return service.DeleteQuestionAnswer(questionId, answerId);
        }
    }
}
