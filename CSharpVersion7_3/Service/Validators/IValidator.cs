using Common.DataBase.Entities;
using CSharpVersion7_3.Service.Requests;
using System.Collections.Generic;

namespace CSharpVersion7_3.Service.Validators
{
    interface IValidator
    {
        void ValidateRequest(GetUsersByRatingRequest request);
        void ValidateResponse(IEnumerable<UserRating> users);
    }
}