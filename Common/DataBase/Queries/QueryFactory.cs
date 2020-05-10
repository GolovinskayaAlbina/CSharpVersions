namespace Common.DataBase.Queries
{
    class QueryFactory
    {
        public static string UsersByRatingQuery(int start, int end)
        {
            return string.Format("SELECT fullName, rating FROM USERS where rating >= {0} AND <= {1} ORDER BY rating",
                start, end);
        }
    }
}
