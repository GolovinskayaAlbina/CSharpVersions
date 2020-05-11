using Common.DataBase.Entities;
using System;
using System.Collections.Generic;

namespace CSharpVersion5.Service.Responses
{
    public class GetUsersByRatingResponse
    {
        public IEnumerable<UserRating> Users { get; private set; }
        public GetUsersByRatingResponse(IEnumerable<UserRating> users)
        {
            Users = users;
        }
    }
}