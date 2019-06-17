namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ITipoNaturezaNegocio
    {
        #region CRUD

        List<TipoNatureza> GetAll();
        TipoNatureza FindById(int id);
        TipoNatureza Save(TipoNatureza tipoNatureza);

        #endregion CRUD

        bool ExistsTipoNatureza(string natureza);
    }
}
