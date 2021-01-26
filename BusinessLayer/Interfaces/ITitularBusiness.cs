using System.Collections.Generic;
using System.Data.SqlClient;
using DTOLayer;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// The Cfe4Negocio interface.
    /// </summary>
    public interface ITitularBusiness /*: IRepository<CFE4>*/
    {
        /// <summary>The insert or update.</summary>
        /// <param name="processos">The processos.</param>
        /// <param name="transaction">The transaction.</param>
        void InsertOrUpdate(List<ProcessoImported> processos, SqlTransaction transaction);
    }
}