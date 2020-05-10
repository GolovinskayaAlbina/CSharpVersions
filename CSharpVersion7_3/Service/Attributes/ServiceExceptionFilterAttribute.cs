using Common.RestApi;
using System;
using System.Data.Common;
using System.Net;
using ExceptionFilterAttribute = Common.RestApi.Attributes.ExceptionFilterAttribute;

namespace CSharpVersion7_3.Service.Attributes
{
    public class ServiceExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(Exception exception)
        {
            //7.0 Pattern matching <code>case SomeType x</code>, <code>case SomeType x when x.y</code>
            switch (exception)
            {
                //7.0 Discards <code>case SomeType _</code>
                case NotImplementedException _:
                    Response.ThrowResponseException(HttpStatusCode.NotImplemented);
                    break;
                case ArgumentOutOfRangeException argOutEx:
                    Response.ThrowResponseException(HttpStatusCode.NoContent, argOutEx.Message);
                    break;
                case ArgumentNullException argNullEx:
                    Response.ThrowResponseException(HttpStatusCode.BadRequest, argNullEx.Message);
                    break;
                case ArgumentException argEx when string.IsNullOrEmpty(argEx.Message):
                    Response.ThrowResponseException(HttpStatusCode.BadRequest, "Invalid argument");
                    break;
                case ArgumentException argEx:
                    Response.ThrowResponseException(HttpStatusCode.BadRequest, argEx.Message);
                    break;
                case DbException _:
                    Response.ThrowResponseException(HttpStatusCode.ServiceUnavailable);
                    break;
                case Exception _:
                    Response.ThrowResponseException(HttpStatusCode.InternalServerError);
                    break;
            };
        }
    }
}
