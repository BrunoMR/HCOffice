using System;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class ProcessoRepository : ARepository
    {
        public void InsertOrUpdate(DataTable modelDataTable, SqlTransaction transaction)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_PROCESSO"))
                {
                    var connection = Conexao.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableProcessos", modelDataTable);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível inserir os Processo ", ex.InnerException);
            }
        }
        
    }
}
