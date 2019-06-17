namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;
    using System.Threading.Tasks;

    public interface IClienteNegocio
    {
        #region CRUD

        Task<List<Cliente>> GetAllAsync();
        Task<Cliente> FindByIdAsync(int id);
        Cliente AddOrUpdate(Cliente cliente);
        void Remove(int id);

        #endregion CRUD

        Task<Cliente> FindByIdLoadAsync(int id);
    }
}
