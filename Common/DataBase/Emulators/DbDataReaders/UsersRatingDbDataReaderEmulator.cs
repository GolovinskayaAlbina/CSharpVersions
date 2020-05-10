using Common.DataBase.Entities;
using System.Collections.Generic;

namespace Common.DataBase.Emulators.DbDataReaders
{
    public class UsersRatingDbDataReaderEmulator : DbDataReaderEmulator
    {
        private readonly IEnumerator<UserRating> _usersRatingEnumerator;
        private int _rating = 0;

        public UsersRatingDbDataReaderEmulator(IEnumerable<UserRating> userRatingCollection)
        {
            _usersRatingEnumerator = userRatingCollection.GetEnumerator();
        }

        public override string GetString(int i)
        {
            return _usersRatingEnumerator.Current.FullName;
        }

        public override int GetInt32(int i)
        {
            return ++_rating;
        }

        public override bool Read()
        {
            return _usersRatingEnumerator.MoveNext();
        }
    }
}