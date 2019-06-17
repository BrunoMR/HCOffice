namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;
    using System.Threading.Tasks;

    public interface IPaisNegocio
    {
        #region CRUD

        List<Pais> GetAllAsync();
        Task<Pais> FindByIdAsync(int id);
        Pais AddOrUpdate(Pais pais);
        void Remove(int id);

        #endregion CRUD

        bool CountryExists(string sigla);
    }
}
