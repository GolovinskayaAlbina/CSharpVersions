namespace Common.DataBase.Entities
{
    public class UserRating
    {
        public string FullName { get; set; }
        public int Rating { get; set; }

        public override string ToString()
        {
            return string.Format("{0,3}: {1}", Rating, FullName);
        }
    }
}