using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Interfaces.IUser;

namespace EvaluationSystem.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : AuthorizeUserController
    {
        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<List<UserDto>> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("GetUsersToEvaluate")]
        public IEnumerable<ExposeUserDto> GetUsersToEvaluate()
        {
            return _service.GetUsersToEvaluate();
        }
    }
}
