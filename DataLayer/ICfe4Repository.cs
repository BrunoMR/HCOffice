namespace DataLayer
{
    using System.Data;
    using System.Data.SqlClient;
    using DTOLayer;

    /// <summary>
    /// The Cfe4Repository interface.
    /// </summary>
    public interface ICfe4Repository : IRepository<CFE4>
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
