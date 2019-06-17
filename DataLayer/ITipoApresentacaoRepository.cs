using System.Collections.Generic;
using System.Text;
using Dapper;
using DTOLayer;

namespace DataLayer
{
    public interface ITipoApresentacaoRepository
    {
        #region CRUD

        List<TipoApresentacao> GetAll();
        TipoApresentacao FindById(int id);
        TipoApresentacao Add(TipoApresentacao tipoNatureza);
        TipoApresentacao Update(TipoApresentacao tipoNatureza);
        void Remove(int id);

        #endregion CRUD
    }
}
