using EvaluationSystem.Application.Exceptions;
using EvaluationSystem.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSystem.Application.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await HandleException(ex, context);
            }
        }

        private async Task HandleException(Exception ex, HttpContext context)
        {
            var errors = new List<Error>();

            switch (ex)
            {
                case HttpException httpException:
                    errors.Add(new Error { Code = (int)httpException.StatusCode, ErrorMessage = httpException.Message });
                    break;
                case ValidationException validationException:
                    errors.AddRange(validationException.Errors.Select(e => new Error { Code = 400, ErrorMessage = e.ErrorMessage }));
                    break;
                default:
                    errors.Add(new Error { Code = 500, ErrorMessage = ex.Message });
                    break;
            }

            var errorsSet = JsonConvert.SerializeObject(errors);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errors[0].Code;
            await context.Response.WriteAsync(errorsSet);
        }
    }
}
