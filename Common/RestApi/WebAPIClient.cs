using Common.DIContainers;
using System;
using System.Threading.Tasks;

namespace Common.RestApi
{
    public class WebAPIClient: IDisposable
    {
        private readonly ServiceApi _api;

        //7.0 More expression-bodied members
        public WebAPIClient(Container container) => _api = new ServiceApi(container);

        public async Task<Response> GetAsync<Response,Request>(string path, Request request)
        {
            return await _api.CallMethodAsync<Response, Request>(path, request);
        }

        public void Dispose() => _api.Dispose();
    }
}
