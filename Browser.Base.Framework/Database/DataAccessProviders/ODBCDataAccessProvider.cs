using System;
using System.Data.Odbc;
using System.Data.Common;
using System.Net.Sockets;


namespace Browser.Core.Framework
{
    /// <summary>
    /// An Impala Cloudera CData Apache implementation of the DataAccessProvider.
    /// </summary>
    public class ODBCDataAccessProvider : DataAccessProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">The string used to connect to the data source.</param>
        public ODBCDataAccessProvider(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Using the connection string, connect to a data source.
        /// </summary>
        /// <returns>A connection to the data source.</returns>
        public override DbConnection CreateConnection()
        {
            return new OdbcConnection(ConnectionString);
        }

        /// <summary>
        /// Create a DataAdapter for the data source.
        /// </summary>
        /// <returns>A DataAdapter for the data source.</returns>
        public override DbDataAdapter CreateAdapter()
        {
            //Socket socket = null;
            //TTransport transport = null;

            //socket = new Socket("localhost", 9160);


            //transport = new TFramedTransport(socket);
            //TProtocol protocol = new TBinaryProtocol(transport);
            //CassandraClient cassandraClient = new CassandraClient(protocol);
            //cassandraClient.InputProtocol.Transport.Open();

            return new OdbcDataAdapter();
        }

        /// <summary>
        /// Given a data adapter, create a single table command builder.
        /// </summary>
        /// <param name="adapter"></param>
        /// <returns>A commmand builder.</returns>
        public override DbCommandBuilder CreateCommandBuilder(DbDataAdapter adapter)
        {
            if (adapter == null) throw new ArgumentNullException(nameof(adapter));
            return new OdbcCommandBuilder((OdbcDataAdapter)adapter);
        }
    }
}
