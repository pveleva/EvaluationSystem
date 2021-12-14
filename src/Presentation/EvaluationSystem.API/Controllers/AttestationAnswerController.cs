using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/attestationAnswer")]
    [ApiController]
    public class AttestationAnswerController : AuthorizeControllerBase
    {
        private IAttestationAnswerService _service;
        public AttestationAnswerController(IAttestationAnswerService service)
        {
            _service = service;
        }
        [HttpPost()]
        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            return _service.Create(createAttestationAnswerDto);
        }
    }
}
