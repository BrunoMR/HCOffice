using System;
using System.Data;
using System.Data.SqlClient;
using DataLayer.Connections;
using DataLayer.Interfaces;
using DTOLayer;

namespace DataLayer.Implementations
{
    /// <summary>
    /// The cfe 4 repository.
    /// </summary>
    public class Cfe4Repository : Repository<CFE4>, ICfe4Repository
    {
        /// <summary>
        /// Método irá chamar a Procedure "UPDATE_PROCESSO_CFE4" passando um tabela populada com os códigos CFE4 do Processo
        /// que na Procedure irá excluir os códigos CFE4 do Processo e inserir os novos
        /// </summary>
        /// <param name="dataTableModel">Tabela com códigos CFE4 do Processo</param>
        /// <param name="transaction">Transação utilizada</param>
        public void BulkUpsert(DataTable dataTableModel, SqlTransaction transaction)
        {
            try
            {
                using (var cmd = new SqlCommand("UPDATE_PROCESSO_CFE4"))
                {
                    var connection = ConnectionDapper.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableCfe4s", dataTableModel);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Não foi possível inserir as Classes Vienna ", ex.InnerException);
            }
        }
    }
}