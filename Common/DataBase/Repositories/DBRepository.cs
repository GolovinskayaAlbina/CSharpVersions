using Common.DataBase.Emulators;
using Common.DataBase.Entities;
using Common.DataBase.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Common.DataBase.Repositories
{
    public class DBRepository : IRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DBRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<IEnumerable<UserRating>> GetUsersByRatingAsync(int start, int end)
        {
            return await GetDataCollectionFromDB(QueryFactory.UsersByRatingQuery(start, end), (reader) =>
            {
                return new UserRating
                {
                    FullName = reader.GetString(0),
                    Rating = reader.GetInt32(1)
                };
            });
        }

        private async Task<IEnumerable<T>> GetDataCollectionFromDB<T>(string query, Func< DbDataReader, T> converter)
        {
            var result = new List<T>();
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(converter(reader));
                    }
                }
            }
            return result;
        }
    }
}