using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataLayer.Connections;
using DTOLayer;
using ServiceStack;
using ServiceStack.OrmLite;

namespace DataLayer
{
    public class ClienteRepository : ARepository, IClienteRepository
    {
        #region CRUD

        public async Task<List<Cliente>> GetAllAsync()
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().SelectAsync<Cliente>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Cliente!", ex.InnerException);
            }
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().SingleByIdAsync<Cliente>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Cliente com ID = '{0}'!", id), ex.InnerException);
            }
        }

        public Cliente Add(Cliente cliente)
        {
            try
            {
                cliente.Id = (int)ConnectionOrmLite.OpenConnection().Insert(cliente, true);
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível inserir o Cliente '{cliente.Nome}'!", ex.InnerException);
            }
        }

        public Cliente Update(Cliente cliente)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(cliente);
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar o Cliente '{0}'!", cliente.Nome), ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<Cliente>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível excluir o Cliente com ID = '{0}'!", id), ex.InnerException);
            }
        }

        public async Task<Cliente> FindByIdLoadAsync(int id)
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().LoadSingleByIdAsync<Cliente>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Cliente com ID = '{0}'!", id), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}