using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer.Connections
{
    public class ConnectionDapper
    {
        private static ConnectionDapper _instancia;
        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString);
        public static ConnectionDapper RetornaInstancia()
        {
            return _instancia ?? (_instancia = new ConnectionDapper());
        }

        public SqlConnection AbreConexao()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
            }
            catch (SqlException e)
            {
                throw new Exception("Não foi possível conectar no banco de Dados!", e.InnerException);
            }

            return _connection;
        }

        public void FechaConexao()
        {
            try
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
            }
            catch (SqlException e)
            {
                throw new Exception("Não foi possível fechar a conexão com o banco de Dados!", e.InnerException);
            }
        }
    }
}
