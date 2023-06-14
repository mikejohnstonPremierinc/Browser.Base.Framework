//using System;
//using System.Data.Odbc;
//using System.Data.Common;
//using System.Net.Sockets;
////using Robbiblubber.Extend.SQL.JDBC.Data;

//namespace Browser.Core.Framework
//{
//    /// <summary>
//    /// A JDBC implementation of the DataAccessProvider.
//    /// </summary>
//    public class JDBCDataAccessProvider : DataAccessProvider_Java
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="connectionString">The string used to connect to the data source.</param>
//        public JDBCDataAccessProvider(string connectionString) : base(connectionString) { }

//        /// <summary>
//        /// Using the connection string, connect to a data source.
//        /// </summary>
//        /// <returns>A connection to the data source.</returns>
//        public override JdbcConnection CreateConnectionJDBC()
//        {          
//            return new JdbcConnection(ConnectionString);
//        }

//        /// <summary>
//        /// Create a DataAdapter for the data source.
//        /// </summary>
//        /// <returns>A DataAdapter for the data source.</returns>
//        public override JdbcDataAdapter CreateAdapter()
//        {
//            return new JdbcDataAdapter();
//        }

//        /// <summary>
//        /// Given a data adapter, create a single table command builder.
//        /// </summary>
//        /// <param name="adapter"></param>
//        /// <returns>A commmand builder.</returns>
//        public override DbCommandBuilder CreateCommandBuilder(DbDataAdapter adapter)
//        {
//            if (adapter == null) throw new ArgumentNullException(nameof(adapter));
//            return new OdbcCommandBuilder((OdbcDataAdapter)adapter);
//        }
//    }
//}
