namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ITipoSituacaoRepository
    {
        #region CRUD

        List<TipoSituacao> GetAll();
        TipoSituacao FindById(int id);
        TipoSituacao Add(TipoSituacao tipoSituacao);
        TipoSituacao Update(TipoSituacao tipoSituacao);
        void Remove(int id);

        #endregion CRUD
    }
}
