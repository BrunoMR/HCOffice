namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;
    using System.Threading.Tasks;

    public interface IEstadoNegocio
    {
        #region CRUD

        List<Uf> GetAllAsync();
        Task<Uf> FindByIdAsync(int id);
        Uf Save(Uf uf);
        void Remove(int id);

        #endregion CRUD
    }
}
