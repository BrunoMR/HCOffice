using System.Data;
using System.Data.SqlClient;

namespace DataLayer.Interfaces
{
    /// <summary>
    /// The TitularRepository interface.
    /// </summary>
    public interface ITitularRepository
    {
        /// <summary>
        /// The bulk upsert.
        /// </summary>
        /// <param name="dataTableModel">
        /// The data table model.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        void BulkUpsert(DataTable dataTableModel, SqlTransaction transaction);
    }
}