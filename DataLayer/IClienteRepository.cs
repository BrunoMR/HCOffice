namespace DataLayer
{
    using System.Collections.Generic;
    using DTOLayer;
    using System.Threading.Tasks;

    public interface IClienteRepository
    {
        #region CRUD

        Task<List<Cliente>> GetAllAsync();
        Task<Cliente> FindByIdAsync(int id);
        Cliente Add(Cliente cliente);
        Cliente Update(Cliente cliente);
        void Remove(int id);

        #endregion CRUD

        Task<Cliente> FindByIdLoadAsync(int id);
    }
}
