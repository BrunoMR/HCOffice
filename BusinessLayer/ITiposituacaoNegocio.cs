namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ITipoSituacaoNegocio
    {
        #region CRUD

        List<TipoSituacao> GetAll();
        TipoSituacao FindById(int id);
        TipoSituacao AddOrUpdate(TipoSituacao tipoSituacao);
        void Remove(int id);

        #endregion CRUD
    }
}
