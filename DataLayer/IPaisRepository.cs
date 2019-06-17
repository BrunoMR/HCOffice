namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;
    using System.Threading.Tasks;

    public interface IPaisRepository
    {
        #region CRUD

        List<Pais> GetAllAsync();
        Task<Pais> FindByIdAsync(int id);
        Pais Add(Pais pais);
        Pais Update(Pais pais);
        void Remove(int id);

        #endregion CRUD
    }
}
