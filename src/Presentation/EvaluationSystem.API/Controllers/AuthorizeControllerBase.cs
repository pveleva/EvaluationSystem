using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EvaluationSystem.API.Controllers
{    
    [Authorize]
    public abstract class AuthorizeControllerBase : ControllerBase
    {
    }
}
