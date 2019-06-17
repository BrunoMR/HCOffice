namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface IDespachoRepository
    {
        #region CRUD

        List<Despacho> GetAll();
        Despacho FindById(int id);
        Despacho Add(Despacho despacho);
        Despacho Update(Despacho despacho);
        void Remove(int id);

        #endregion CRUD
    }
}
