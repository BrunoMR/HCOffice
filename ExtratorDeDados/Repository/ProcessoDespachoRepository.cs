using System;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class ProcessoDespachoRepository : ARepository
    {
        public void BulkInsert(DataTable dataTablemodel, SqlTransaction transaction)
        {
            try
            {
                using (var cmd = new SqlCommand("INSERT_PROCESSO_DESPACHO"))
                {
                    var connection = Conexao.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableProcessoDespachos", dataTablemodel);
                    cmd.Transaction = transaction;
                    cmd.CommandTimeout = 1200;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível inserir os Processo Despachos", ex.InnerException);
            }
        }
        
    }
}
