using System.Threading.Tasks;

namespace Common.RestApi
{
    public class WebAPIClient
    {
        public async Task<Response> GetAsync<Response,Request>(string path, Request request)
        {
            return await ServiceApi.CallMethodAsync<Response, Request>(path, request);
        }
    }
}
