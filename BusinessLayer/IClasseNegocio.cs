namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface IClasseNegocio
    {
        #region CRUD

        List<Classe> GetAll();

        Classe FindByCode(string code);

        Classe Save(Classe classe);

        #endregion CRUD
    }
}