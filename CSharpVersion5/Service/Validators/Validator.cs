using Common.DataBase.Entities;
using CSharpVersion5.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpVersion5.Service.Validators
{
    class Validator : IValidator
    {
        public void ValidateRequest(GetUsersByRatingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (request.Start == 0)/*first position should be more or equels 1*/
            {
                throw new ArgumentException("Start");
            }
            if (request.End - request.Start < 0)
            {
                throw new ArgumentException("End");
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