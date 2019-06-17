using System.Collections.Generic;
using System.Text;
using Dapper;
using DTOLayer;

namespace DataLayer
{
    public interface ITipoNaturezaRepository
    {
        #region CRUD

        List<TipoNatureza> GetAll();
        TipoNatureza FindById(int id);
        TipoNatureza Add(TipoNatureza tipoNatureza);
        TipoNatureza Update(TipoNatureza tipoNatureza);
        void Remove(int id);

        #endregion CRUD
        
    }
}
