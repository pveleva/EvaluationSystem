using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces.IUser;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet()]
        public IEnumerable<User> GetAll()
        {
            return _service.GetAll();
        }
    }
}
