using Common.DataBase.Entities;
using Common.DataBase.Queries;
using Ploeh.AutoFixture;
using System;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.DataBase.Emulators.DbDataReaders
{
    public class DbDataReaderEmulatorFactory
    {
        public DbDataReader Create(string commandText)
        {
            var fixture = new Fixture();
            int start;
            int end;
            if (IsUsersByRatingQuery(commandText, out start, out end))
            {
                return CreateDbDataReaderWithUsersByRating(start, end, fixture);
            }

            throw new NotSupportedException();
        }

        private static DbDataReader CreateDbDataReaderWithUsersByRating(int start, int end, IFixture fixture)
        {
            return new UsersRatingDbDataReaderEmulator(fixture.CreateMany<UserRating>(end - start + 1));
        }

        private static bool IsUsersByRatingQuery(string commandText, out int start, out int end)
        {
            start = 0;
            end = 0;
            var matches = Regex.Matches(commandText, "\\d+")
                .Cast<Match>()
                .Select(x => int.Parse(x.Value))
                .ToList();

            if (matches.Count == 2) 
            {
                start = matches[0];
                end = matches[1];

                return commandText.Equals(QueryFactory.UsersByRatingQuery(start, end));
            }

            return false;
        }
    }
}