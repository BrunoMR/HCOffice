using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;

namespace DataLayer.Connections
{
    public class OrmLiteConnection
    {
        private static OrmLiteConnection _instancia;

        private readonly OrmLiteConnectionFactory _connectionFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString, SqlServerDialect.Provider);
	    private IDbConnection _connection;

		public OrmLiteConnection()
		{
			_connection = _connectionFactory.OpenDbConnection();
		}

		public static OrmLiteConnection GetInstance()
        {
            return _instancia ?? (_instancia = new OrmLiteConnection());
        }

        public IDbConnection OpenConnection()
        {
            try
            {
                return _connectionFactory.OpenDbConnection();
            }
            catch (SqlException e)
            {
                throw new Exception("Não foi possível conectar no banco de Dados!", e.InnerException);
            }
        }

	    public IDbConnection Connection
	    {
		    get
		    {
				if(_connection.State != ConnectionState.Open)
					_connection = _connectionFactory.OpenDbConnection();

			    return _connection;
		    }
	    }
	}
}