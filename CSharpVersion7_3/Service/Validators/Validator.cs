using Common.DataBase.Entities;
using Common.RestApi.Requests;
using Common.RestApi.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpVersion7_3.Service.Validators
{
    class Validator : IValidator
    {
        //6.0 The nameof expression <code>nameof(x)</code>
        public void ValidateRequest(GetUsersByRatingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Start == 0)/*first position should be more or equels 1*/
            {
                throw new ArgumentException(nameof(request.Start));
            }
            if (request.End - request.Start < 0)
            {
                throw new ArgumentException(nameof(request.End));
            }
        }

        public void ValidateResponse(IEnumerable<UserRating> users)
        {
            if (users.Count() == 0)
            {
                throw new ArgumentOutOfRangeException("No users matching criteria");
            }
        }
    }
}