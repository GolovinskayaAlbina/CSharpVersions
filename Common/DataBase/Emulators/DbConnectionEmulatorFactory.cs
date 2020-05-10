using Common.DataBase.Emulators.DbDataReaders;
using System.Data.Common;

namespace Common.DataBase.Emulators
{
    public class DbConnectionEmulatorFactory: IDbConnectionFactory
    {
        public DbConnection CreateConnection()
        {
            return new DbConnectionEmulator(new DbDataReaderEmulatorFactory());
        }
    }
}