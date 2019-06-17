using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTOLayer;
using System.Threading.Tasks;

namespace DataLayer
{
    using PagedList;

    public interface IProcessoRepository
    {
        #region CRUD

        Task<List<Processo>> GetAll();
        List<Processo> GetAll(int pageNumber, int pageSize, out int count);
        Task<Processo> FindById(int id);
        Processo Add(Processo processo);
        Processo Update(Processo processo);
        void Remove(int id);

        #endregion CRUD

        Task<Processo> FindByNumeroProcesso(string numero);

        Task<Processo> GetAllOfProcessoByNumeroAsync(string numero);

        void BulkInsertOrUpdate(DataTable modelDataTable, SqlTransaction transaction);
    }
}
