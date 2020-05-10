using Common.DataBase.Emulators.DbDataReaders;
using System;
using System.Data;
using System.Data.Common;

namespace Common.DataBase.Emulators
{
    public class DbConnectionEmulator : DbConnection
    {
        private readonly DbDataReaderEmulatorFactory _readerFactory;

        public DbConnectionEmulator(DbDataReaderEmulatorFactory readerFactory)
        {
            _readerFactory = readerFactory;
        }

        public override string ConnectionString { get ; set ; }

        public override string Database { get { throw new NotImplementedException(); } }

        public override string DataSource { get { throw new NotImplementedException(); } }

        public override string ServerVersion { get { throw new NotImplementedException(); } }

        public override ConnectionState State { get { throw new NotImplementedException(); } }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            /*nothing to do*/
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        protected override DbCommand CreateDbCommand()
        {
            return new DbCommandEmulator(_readerFactory);
        }
    }
}