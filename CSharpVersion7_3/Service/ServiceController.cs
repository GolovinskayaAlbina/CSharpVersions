using Common.DataBase.Repositories;
using Common.RestApi.Emulators.Controllers;
using Common.RestApi.Requests;
using Common.RestApi.Responses;
using Common.RestApi.Validators;
using CSharpVersion7_3.Service.Attributes;
using System.Threading.Tasks;

namespace CSharpVersion7_3.Service
{
    class ServiceController : ControllerEmulator
    {
        private readonly IRepository _repository;
        private readonly IValidator _validator;

        public ServiceController(IRepository repository, IValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        [ServiceExceptionFilter]
        public async Task<GetUsersByRatingResponse> GetUsersByRating(GetUsersByRatingRequest request)
        {
            _validator.ValidateRequest(request);

            var users = await _repository.GetUsersByRatingAsync(request.Start, request.End);

            _validator.ValidateResponse(users);
            return new GetUsersByRatingResponse { Users = users };
        }
    }
}
