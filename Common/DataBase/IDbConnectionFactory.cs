using System.Data.Common;

namespace Common
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
    }
}