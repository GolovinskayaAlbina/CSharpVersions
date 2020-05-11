using Common.DataBase.Repositories;
using Common.RestApi.Emulators.Controllers;
using CSharpVersion7_3.Service.Attributes;
using CSharpVersion7_3.Service.Requests;
using CSharpVersion7_3.Service.Responses;
using CSharpVersion7_3.Service.Validators;
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
