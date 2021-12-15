using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IUserAnswer;
using EvaluationSystem.Application.Models.AttestationAnswers;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/attestationAnswer")]
    [ApiController]
    public class AttestationAnswerController : AuthorizeControllerBase
    {
        private IUserAnswerService _service;
        public AttestationAnswerController(IUserAnswerService service)
        {
            _service = service;
        }

        [HttpGet("{idAttestation}")]
        public CreateGetFormDto GetAll(int idAttestation, string email)
        {
            return _service.Get(idAttestation, email);
        }

        [HttpPost()]
        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            return _service.Create(createAttestationAnswerDto);
        }
    }
}
