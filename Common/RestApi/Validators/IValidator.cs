using Common.DataBase.Entities;
using Common.RestApi.Requests;
using System.Collections.Generic;

namespace Common.RestApi.Validators
{
    public interface IValidator
    {
        void ValidateRequest(GetUsersByRatingRequest request);
        void ValidateResponse(IEnumerable<UserRating> users);
    }
}