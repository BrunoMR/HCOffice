namespace BusinessLayer
{
    using System.Collections.Generic;
    using DataLayer;
    using DTOLayer;
    using System;
    using System.Threading.Tasks;

    public class ClienteNegocio : IClienteNegocio
    {
        readonly IClienteRepository _clienteRepository = new ClienteRepository();

        #region CRUD

        public async Task<List<Cliente>> GetAllAsync()
        {
            try
            {
                return await _clienteRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            try
            {
                return await _clienteRepository.FindByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Cliente Add(Cliente cliente)
        {
            try
            {
                return _clienteRepository.Add(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Cliente Update(Cliente cliente)
        {
            try
            {
                return _clienteRepository.Update(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Cliente AddOrUpdate(Cliente cliente)
        {
            return cliente?.Id == null
                ? Add(cliente)
                : Update(cliente);
        }

        public void Remove(int id)
        {
            try
            {
                _clienteRepository.Remove(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion CRUD

        public Task<Cliente> FindByIdLoadAsync(int id)
        {
            try
            {
                return _clienteRepository.FindByIdLoadAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
