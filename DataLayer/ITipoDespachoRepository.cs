namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ITipoDespachoRepository
    {
        #region CRUD

        List<TipoDespacho> GetAll();
        TipoDespacho FindByTipo(char tipo);
        TipoDespacho Add(TipoDespacho tipoDespacho);
        TipoDespacho Update(TipoDespacho tipoDespacho);
        void Remove(char tipo);

        #endregion CRUD
    }
}
