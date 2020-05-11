using Common.DataBase.Entities;
using Common.RestApi;
using CSharpVersion7_3.Service.Requests;
using CSharpVersion7_3.Service.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace CSharpVersion7_3.Client
{
    class Client
    {
        private readonly WebAPIClient _webAPIClient;

        //7.0 More expression-bodied members
        public Client(WebAPIClient webAPIClient) => _webAPIClient = webAPIClient;

        public async Task PringUsersByRatingAsync(int start, int end)
        {
            var request = new GetUsersByRatingRequest(start,end);

            var response = await GetFromServiceSafely<GetUsersByRatingResponse, GetUsersByRatingRequest>(
                "service/GetUsersByRating", request);

            //6.0 Null-conditional operators <code>x?.y</code>
            Console.WriteLine(string.Join(Environment.NewLine, response?.Users ?? new List<UserRating>()));
        }

        private async Task<Res> GetFromServiceSafely<Res,Req>(string path, Req request)
        {
            try
            {
                return await _webAPIClient.GetAsync<Res, Req>(path, request);
            }
            //6.0 Exception filters <code>catch(x) when (x.y)</code>
            catch (HttpResponseException ex) when (ex.Response.StatusCode == HttpStatusCode.BadRequest)
            {
                //6.0 Await in Catch and Finally blocks
                await LogException(ex);
            }
            catch (HttpResponseException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return default;
        }

        private static async Task LogException(HttpResponseException exception)
        {
            //6.0 String interpolation <code>$"{x}: {y}"</code>
            await Task.Run(() => Console.WriteLine($"{exception.Response.StatusCode}: {exception.Response.ReasonPhrase}"));
        }
    }
}
