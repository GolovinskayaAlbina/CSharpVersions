using Common.DataBase.Entities;
using System.Collections.Generic;

namespace Common.RestApi.Responses
{
    public class GetUsersByRatingResponse
    {
        public IEnumerable<UserRating> Users { get; set; }
    }
}