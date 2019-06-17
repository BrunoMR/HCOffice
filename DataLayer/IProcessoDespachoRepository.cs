namespace DataLayer
{
    using DTOLayer;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public interface IProcessoDespachoRepository
    {
        void BulkInsert(DataTable dataTablemodel, SqlTransaction transaction);

        Task<List<ProcessoDespacho>> GetAllOfProcesso(ProcessoDespacho processoDespacho);

        Task<List<ProcessoDespacho>> ProcessoDespachoLoadByProcessoIdAsync(int processoId);
    }
}
