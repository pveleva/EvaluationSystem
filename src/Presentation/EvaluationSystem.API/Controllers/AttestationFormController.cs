using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IAttestationForm;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/attestationForm")]
    [ApiController]
    public class AttestationFormController : AuthorizeControllerBase
    {
        private IAttestationFormService _attestationFormService;
        public AttestationFormController(IAttestationFormService attestationFormService)
        {
            _attestationFormService = attestationFormService;
        }

        [HttpGet("{idForm}")]
        public CreateGetFormDto Get(int idForm)
        {
            return _attestationFormService.GetAll().Where(f => f.Id == idForm).FirstOrDefault();
        }
    }
}
