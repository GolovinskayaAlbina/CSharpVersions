using Common.DataBase.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.DataBase.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<UserRating>> GetUsersByRatingAsync(int start, int end);
    }
}