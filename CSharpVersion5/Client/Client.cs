using Common.RestApi;
using CSharpVersion5.Service.Requests;
using CSharpVersion5.Service.Responses;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace CSharpVersion5.Client
{
    class Client
    {
        private readonly WebAPIClient _webAPIClient;

        public Client(WebAPIClient webAPIClient)
        {
            _webAPIClient = webAPIClient;
        }

        public async Task PringUsersByRatingAsync(int start, int end)
        {
            var request = new GetUsersByRatingRequest
            {
                Start = start,
                End = end
            };

            var response = await GetFromServiceSafely<GetUsersByRatingResponse, GetUsersByRatingRequest>(
                "service/GetUsersByRating", request);

            if (response != null && response.Users != null)
            {
                Console.WriteLine(string.Join(Environment.NewLine, response.Users));
            }
        }

        private async Task<Res> GetFromServiceSafely<Res, Req>(string path, Req request)
        {
            try
            {
                return await _webAPIClient.GetAsync<Res, Req>(path, request);
            }
            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine(string.Format("{0}: {1}", ex.Response.StatusCode, ex.Response.ReasonPhrase));
                }
                else
                {
                    throw new Exception(ex.Message, ex);
                }
            }
            return default(Res);
        }
    }
}
