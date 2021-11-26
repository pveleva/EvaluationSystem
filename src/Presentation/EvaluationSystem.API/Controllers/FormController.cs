using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IForm;
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
        public List<GetFormDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{formId}")]
        public GetFormDto GetById(int formId)
        {
            return _service.GetById(formId);
        }

        [HttpPost()]
        public ExposeFormDto Create(CreateUpdateFormDto formDto)
        {
            return _service.Create(formDto);
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
