using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ExtratorDeDados.Util
{
    public class Conexao
    {
        private static Conexao _instancia;
        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ToString());
        public static Conexao RetornaInstancia()
        {
            return _instancia ?? (_instancia = new Conexao());
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
