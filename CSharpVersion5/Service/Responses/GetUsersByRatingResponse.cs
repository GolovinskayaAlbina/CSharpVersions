using Common.DataBase.Entities;
using System.Collections.Generic;

namespace CSharpVersion5.Service.Responses
{
    public class GetUsersByRatingResponse
    {
        public IEnumerable<UserRating> Users { get; set; }
    }
}