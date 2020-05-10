using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Common.RestApi
{
    public class Response
    {
        static HttpResponseMessage CreateErrorResponse(HttpStatusCode status, string message = null)
        {
            return new HttpResponseMessage
            {
                StatusCode = status,
                ReasonPhrase = message
            };
        }

        public static HttpResponseMessage ThrowResponseException(HttpStatusCode status, string message = null)
        {
            throw new HttpResponseException(CreateErrorResponse(status, message));
        }
    }
}
