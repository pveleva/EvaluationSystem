using EvaluationSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EvaluationSystem.API.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public Error Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception
            var code = 500; // Internal Server Error by default

            if (HttpContext.Response is NotFoundObjectResult) code = 404; // Not Found
            else if (context is BadRequestObjectResult) code = 404; // Unauthorized
            else if (exception is UnauthorizedAccessException) code = 401; // Unauthorized
            else if (exception is Exception) code = 400; // Bad Request

            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            return new Error();//exception); // Your error model
        }
    }
}
