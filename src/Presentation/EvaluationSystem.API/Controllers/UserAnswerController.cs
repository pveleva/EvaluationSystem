using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IUserAnswer;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Interfaces.IAttestationForm;
using System.Linq;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/attestationAnswer")]
    [ApiController]
    public class UserAnswerController : AuthorizeControllerBase
    {
        private IUserAnswerService _userAnswerService;
        private IAttestationFormService _attestationFormService;
        public UserAnswerController(IUserAnswerService userAnswerService, IAttestationFormService attestationFormService)
        {
            _userAnswerService = userAnswerService;
            _attestationFormService = attestationFormService;
        }

        [HttpGet("{idAttestation}/{email}")]
        public CreateGetFormDto GetAll(int idAttestation, string email)
        {
            return _userAnswerService.Get(idAttestation, email);
        }

        [HttpPost()]
        public GetAttestationAnswerDto Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            return _userAnswerService.Create(createAttestationAnswerDto);
        }
    }
}
