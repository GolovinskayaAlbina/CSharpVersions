using Common.DataBase.Entities;
using CSharpVersion5.Service.Requests;
using System.Collections.Generic;

namespace CSharpVersion5.Service.Validators
{
    interface IValidator
    {
        void ValidateRequest(GetUsersByRatingRequest request);
        void ValidateResponse(IEnumerable<UserRating> users);
    }
}