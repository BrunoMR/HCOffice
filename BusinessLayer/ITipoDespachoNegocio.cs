namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ITipoDespachoNegocio
    {
        #region CRUD

        List<TipoDespacho> GetAll();
        TipoDespacho FindByTipo(char tipo);
        TipoDespacho AddOrUpdate(TipoDespacho tipoDespacho);
        void Remove(char tipo);

        #endregion CRUD
    }
}
