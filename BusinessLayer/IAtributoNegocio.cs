namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface IAtributoNegocio
    {
        #region CRUD

        List<Atributo> GetAll();
        Atributo FindById(int id);
        Atributo Save(Atributo atributo);

        #endregion CRUD

        Atributo FindByCode(string code);
    }
}
