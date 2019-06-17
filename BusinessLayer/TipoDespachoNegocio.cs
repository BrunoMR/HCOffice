namespace BusinessLayer
{
    using System.Collections.Generic;
    using DataLayer;
    using DTOLayer;

    public class TipoDespachoNegocio : ITipoDespachoNegocio
    {
        readonly ITipoDespachoRepository _tipoDespachoRepository = new TipoDespachoRepository();

        #region CRUD

        public List<TipoDespacho> GetAll()
        {
            return _tipoDespachoRepository.GetAll();
        }

        public TipoDespacho FindByTipo(char tipo)
        {
            return _tipoDespachoRepository.FindByTipo(tipo);
        }

        private TipoDespacho Add(TipoDespacho tipoDespacho)
        {
            _tipoDespachoRepository.Add(tipoDespacho);
            return tipoDespacho;
        }

        private TipoDespacho Update(TipoDespacho tipoDespacho)
        {
            return _tipoDespachoRepository.Update(tipoDespacho);
        }

        public TipoDespacho AddOrUpdate(TipoDespacho tipoDespacho)
        {
            return tipoDespacho.IsNew
                ? Add(tipoDespacho)
                : Update(tipoDespacho);
        }

        public void Remove(char tipo)
        {
            _tipoDespachoRepository.Remove(tipo);
        }

        #endregion CRUD
    }
}
