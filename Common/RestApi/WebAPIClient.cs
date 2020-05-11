using Common.DIContainers;
using System.Threading.Tasks;

namespace Common.RestApi
{
    public class WebAPIClient
    {
        private readonly ServiceApi _api;

        public WebAPIClient(Container container)
        {
            _api = new ServiceApi(container);
        }

        public async Task<Response> GetAsync<Response,Request>(string path, Request request)
        {
            return await _api.CallMethodAsync<Response, Request>(path, request);
        }
    }
}
