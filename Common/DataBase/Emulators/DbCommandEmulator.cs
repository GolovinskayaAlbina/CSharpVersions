using Common.DataBase.Emulators.DbDataReaders;
using System;
using System.Data;
using System.Data.Common;

namespace Common.DataBase.Emulators
{
    public class DbCommandEmulator : DbCommand
    {
        private readonly DbDataReaderEmulatorFactory _readerFactory;

        public DbCommandEmulator(DbDataReaderEmulatorFactory readerFactory)
        {
            _readerFactory = readerFactory;
        }

        public override string CommandText { get; set; }
        public override int CommandTimeout { get; set; }
        public override CommandType CommandType { get; set; }
        public override bool DesignTimeVisible { get; set; }
        public override UpdateRowSource UpdatedRowSource { get; set; }
        protected override DbConnection DbConnection { get; set; }

        protected override DbParameterCollection DbParameterCollection { get { throw new NotImplementedException(); } }

        protected override DbTransaction DbTransaction { get; set; }

        public override void Cancel()
        {
            /*nothing to do*/
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            /*nothing to do*/
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return _readerFactory.Create(CommandText);
        }
    }
}