using EvaluationSystem.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;

namespace EvaluationSystem.Application.Validators.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //http://stackoverflow.com/questions/12519561/asp-net-web-api-throw-httpresponseexception-or-return-request-createerrorrespon
        //http://blogs.msdn.com/b/webdev/archive/2012/11/16/capturing-unhandled-exceptions-in-asp-net-web-api-s-with-elmah.aspx

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // (actionExecutedContext.Exception, actionExecutedContext.Request);
            var exception = actionExecutedContext.Exception;
            if (exception != null)
            {
                // we don't handle the http response exception just throw that
                if (exception is HttpResponseException)
                    return;


                if (exception is NotImplementedException)
                    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                else if (exception is ArgumentException)
                {
                    var message = exception.Message.Split(new[] { "\r\n" }, StringSplitOptions.None);
                    ExceptionHandling(HttpStatusCode.BadRequest, message[0]);
                }
                else if (exception is ValidationException)
                    ExceptionHandling(HttpStatusCode.BadRequest, exception.Message);
                else if (exception is ApplicationException)
                    ExceptionHandling(HttpStatusCode.InternalServerError, exception.Message);
                else
                {
                    ExceptionHandling(HttpStatusCode.InternalServerError, @"Internal Server Error");
                }
            }
            base.OnException(actionExecutedContext);
        }

        public void ExceptionHandling(HttpStatusCode statusCode, string message)
        {

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                message = "Internal Server Error. Unable to call downstream system.";
            }
            var errorResponse = new Error
            {
                Code = ((int)statusCode),
                ErrorMessage = message

            };
            var resp = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<Error>(errorResponse, new JsonMediaTypeFormatter(), "application/json")
            };

            throw new HttpResponseException(resp);
        }

        //public override void OnException(HttpActionExecutedContext context)
        //{
        //    string message = "My custom message";
        //    context.Response = context.Request.CreateResponse(
        //        HttpStatusCode.InternalServerError,
        //        new
        //        {
        //            code = 500,
        //            message = message
        //        });
        //}
    }
}

