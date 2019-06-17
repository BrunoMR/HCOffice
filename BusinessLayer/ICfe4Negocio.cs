namespace BusinessLayer
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using DTOLayer;

    /// <summary>
    /// The Cfe4Negocio interface.
    /// </summary>
    public interface ICfe4Negocio /*: IRepository<CFE4>*/
    {
        #region CRUD

        /// <summary>The get all.</summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="count">The count.</param>
        /// <returns>Returns list of Cfe4</returns>
        List<CFE4> GetAll(int pageNumber, int pageSize, out int count);

        /// <summary>The find by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the Cfe4 by Id</returns>
        CFE4 FindById(int id);

        /// <summary>
        /// The add or update.
        /// </summary>
        /// <param name="cfe4">
        /// The cfe 4.
        /// </param>
        /// <returns>
        /// The <see cref="CFE4"/>.
        /// </returns>
        CFE4 AddOrUpdate(CFE4 cfe4);

        #endregion CRUD

        /// <summary>The insert or update.</summary>
        /// <param name="processos">The processos.</param>
        /// <param name="transaction">The transaction.</param>
        void InsertOrUpdate(List<ProcessoImported> processos, SqlTransaction transaction);
    }
}
