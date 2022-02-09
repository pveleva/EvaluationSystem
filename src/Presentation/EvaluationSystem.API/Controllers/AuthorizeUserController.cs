using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [Authorize]
    public abstract class AuthorizeUserController : Controller
    {
    }
}
