using System;
using System.Data.SqlClient;
using System.Text;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class DataBaseRepository : ARepository
    {
        public bool CreateBackupDatabase(string path)
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine(string.Format("BACKUP DATABASE HCOFFICE TO DISK = '{0}'", path));

                using (var cmd = new SqlCommand(query.ToString()))
                {
                    var connection = Conexao.RetornaInstancia();

                    cmd.Connection = connection.AbreConexao();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível Criar o backup do Banco de Dados!", ex.InnerException);
            }
        }
    }
}