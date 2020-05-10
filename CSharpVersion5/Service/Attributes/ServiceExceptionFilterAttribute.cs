using Common.RestApi;
using System;
using System.Data.Common;
using System.Net;
using ExceptionFilterAttribute = Common.RestApi.Attributes.ExceptionFilterAttribute;

namespace CSharpVersion5.Service.Attributes
{
    class ServiceExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(Exception exception)
        {
            if (exception is NotImplementedException)
            {
                Response.ThrowResponseException(HttpStatusCode.NotImplemented);
            }
            else if (exception is ArgumentOutOfRangeException)
            {
                Response.ThrowResponseException(HttpStatusCode.NoContent, exception.Message);
            }
            else if (exception is ArgumentNullException)
            {
                Response.ThrowResponseException(HttpStatusCode.BadRequest, exception.Message);
            }
            else if (exception is ArgumentException)
            {
               Response.ThrowResponseException(HttpStatusCode.BadRequest, exception.Message);
            }
            else if (exception is DbException)
            {
                Response.ThrowResponseException(HttpStatusCode.ServiceUnavailable);
            }
            else if (exception is Exception)
            {
                Response.ThrowResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
