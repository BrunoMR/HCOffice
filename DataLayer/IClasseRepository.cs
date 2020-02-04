using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface IClasseRepository
    {
        #region CRUD

        List<Classe> GetAll();
        Classe FindByCodeClasse(string code);
        void Add(Classe classe);
        Classe Update(Classe classe);
        void Remove(string code);

        #endregion CRUD

        void BulkUpsert(DataTable dataTableModel, SqlTransaction transaction);
    }
}