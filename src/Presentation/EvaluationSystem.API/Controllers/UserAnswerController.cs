using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IUserAnswer;
using EvaluationSystem.Application.Models.AttestationAnswers;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/attestationAnswer")]
    [ApiController]
    public class UserAnswerController : AuthorizeUserController
    {
        private IUserAnswerService _userAnswerService;
        public UserAnswerController(IUserAnswerService userAnswerService)
        {
            _userAnswerService = userAnswerService;
        }

        [HttpGet("{idAttestation}/{email}")]
        public CreateGetFormDto Get(int idAttestation, string email)
        {
            return _userAnswerService.Get(idAttestation, email);
        }

        [HttpPut()]
        public void Update(UpdateAttestationAnswerDto updateAttestationAnswerDto)
        {
            _userAnswerService.Update(updateAttestationAnswerDto);
        }

        [HttpPost()]
        public void Create(CreateAttestationAnswerDto createAttestationAnswerDto)
        {
            _userAnswerService.Create(createAttestationAnswerDto);
        }
    }
}
