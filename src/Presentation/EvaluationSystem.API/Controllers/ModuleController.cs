using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/form/{formId}/module")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private IModuleService _service;
        private IModuleQuestionService _moduleQuestionService;
        public ModuleController(IModuleService service, IModuleQuestionService moduleQuestionService)
        {
            _service = service;
            _moduleQuestionService = moduleQuestionService;
        }

        [HttpGet()]
        public IEnumerable<GetModulesDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{moduleId}")]
        public GetModulesDto GetById(int moduleId)
        {
            return _service.GetById(moduleId);
        }

        [HttpPost()]
        public GetModulesDto Create(GetModulesDto formDto)
        {
            return _service.Create(formDto);
        }

        [HttpPost("{moduleId}/question/{questionId}")]
        public void SetQuestion(int moduleId, int questionId, int position)
        {
            ModuleQuestion moduleQuestion = new ModuleQuestion()
            {
                IdModule = moduleId,
                IdQuestion = questionId,
                Position = position
            };

            _moduleQuestionService.SetQuestion(moduleQuestion);
        }

        [HttpPut("{moduleId}")]
        public ExposeModuleDto Update(int moduleId, UpdateModuleDto formDto)
        {
            return _service.Update(moduleId, formDto);
        }

        [HttpDelete("{moduleId}")]
        public IActionResult Delete(int moduleId)
        {
            _service.DeleteFromRepo(moduleId);
            return StatusCode(204);
        }
    }
}
