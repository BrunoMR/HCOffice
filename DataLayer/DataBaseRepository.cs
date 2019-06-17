using System;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class DataBaseRepository : ARepository, IDataBaseRepository
    {
        public bool CreateBackupDatabase(string path)
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine($"BACKUP DATABASE HCOFFICE TO DISK = '{path}'");

                using (var cmd = new SqlCommand(query.ToString()))
                {
                    cmd.CommandTimeout = 600;
                    cmd.Connection = ConnectionDapper.AbreConexao();
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