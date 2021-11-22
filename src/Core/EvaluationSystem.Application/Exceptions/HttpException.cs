using System;
using System.Net;

namespace EvaluationSystem.Application.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
