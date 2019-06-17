namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;
    using System.Threading.Tasks;

    public interface IEstadoRepository
    {
        #region CRUD

        List<Uf> GetAllAsync();
        Task<Uf> FindByIdAsync(int id);
        Uf Add(Uf uf);
        Uf Update(Uf uf);
        void Remove(int id);

        #endregion CRUD
    }
}
