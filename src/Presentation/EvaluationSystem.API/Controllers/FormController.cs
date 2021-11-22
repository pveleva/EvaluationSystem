using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces.IFormModule;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/form")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private IFormService _service;
        private IFormModuleService _formModuleService;
        public FormController(IFormService service, IFormModuleService formModuleService)
        {
            _service = service;
            _formModuleService = formModuleService;
        }

        [HttpGet()]
        public IEnumerable<CreateUpdateFormDto> GetAll(int questionId)
        {
            return null; //_service.GetAll(questionId);
        }

        [HttpGet("{formId}")]
        public CreateUpdateFormDto GetById(int questionId, int answerId)
        {
            return null; //_service.GetByID(questionId, answerId);
        }

        [HttpPost()]
        public ExposeFormDto Create(CreateUpdateFormDto formDto)
        {
            return _service.Create(formDto);
        }

        [HttpPost("{formId}/module/{moduleId}")]
        public void SetModule(int formId, int moduleId, int position)
        {
            FormModule formModule = new FormModule()
            {
                IdForm = formId,
                IdModule = moduleId,
                Position = position
            };

            _formModuleService.SetModule(formModule);
        }

        [HttpPut("{formId}")]
        public ExposeFormDto Update(int formId, CreateUpdateFormDto formDto)
        {
            return _service.Update(formId, formDto);
        }

        [HttpDelete("{formId}")]
        public IActionResult Delete(int formId)
        {
            _service.DeleteFromRepo(formId);
            return StatusCode(204);
        }
    }
}
