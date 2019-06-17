namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface IDespachoNegocio
    {
        #region CRUD

        List<Despacho> GetAll();
        Despacho FindById(int id);
        Despacho Save(Despacho despacho);

        #endregion CRUD

        /// <summary>The exists despacho.</summary>
        /// <param name="codigo">The codigo.</param>
        /// <returns>Returns if found the Despacho with Code</returns>
        bool ExistsDespacho(string codigo);
    }
}
