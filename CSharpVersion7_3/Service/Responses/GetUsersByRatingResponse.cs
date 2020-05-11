using Common.DataBase.Entities;
using System.Collections.Generic;

namespace CSharpVersion7_3.Service.Responses
{
    public class GetUsersByRatingResponse
    {
        public IEnumerable<UserRating> Users { get; set; }
    }
}