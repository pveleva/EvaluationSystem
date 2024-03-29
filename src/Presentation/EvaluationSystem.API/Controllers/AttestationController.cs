﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Application.Interfaces.IAttestation;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/attestation")]
    [ApiController]
    public class AttestationController : AuthorizeAdminController
    {
        private IAttestationService _service;
        public AttestationController(IAttestationService service)
        {
            _service = service;
        }

        [HttpGet()]
        public IEnumerable<GetAttestationDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpPost]
        public GetAttestationDto Create(CreateAttestationDto attestationDto)
        {
            return _service.Create(attestationDto);
        }

        [HttpDelete("{attestationId}")]
        public IActionResult Delete(int attestationId)
        {
            _service.DeleteFromRepo(attestationId);
            return StatusCode(204);
        }
    }
}
