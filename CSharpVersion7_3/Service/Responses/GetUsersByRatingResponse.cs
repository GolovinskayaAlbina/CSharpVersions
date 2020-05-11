using Common.DataBase.Entities;
using System.Collections.Generic;

namespace CSharpVersion7_3.Service.Responses
{
    class GetUsersByRatingResponse
    {
        //6.0 Read-only auto-properties <code>public T Prop { get; }</code>
        public IEnumerable<UserRating> Users { get; }
        public GetUsersByRatingResponse(IEnumerable<UserRating> users)
        {
            Users = users;
        }
    }
}