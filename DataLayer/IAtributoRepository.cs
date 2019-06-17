namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface IAtributoRepository
    {
        #region CRUD

        List<Atributo> GetAll();
        Atributo FindById(int id);
        Atributo Add(Atributo atributo);
        Atributo Update(Atributo atributo);
        void Remove(int id);

        #endregion CRUD
    }
}
