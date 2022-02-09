using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EvaluationSystem.API.Controllers
{
    [Authorize(Policy = "Admin")]
    public abstract class AuthorizeAdminController : ControllerBase
    {
    }
}
