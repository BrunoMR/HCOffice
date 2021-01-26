using System;
using System.Data;
using System.Data.SqlClient;
using DataLayer.Connections;
using DataLayer.Interfaces;


namespace DataLayer.Implementations
{
    /// <summary>
    /// The titular repository.
    /// </summary>
    public class TitularRepository : ITitularRepository
    {
        public void BulkUpsert(DataTable dataTableModel, SqlTransaction transaction)
        {
            try
            {
                using (var cmd = new SqlCommand("UPDATE_PROCESSO_TITULAR"))
                {
                    var connection = ConnectionDapper.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableTitulares", dataTableModel);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Não foi possível inserir os titulares do processo ", ex.InnerException);
            }
        }
    }
}