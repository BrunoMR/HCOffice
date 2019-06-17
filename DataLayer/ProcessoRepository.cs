using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DataLayer.Connections;
using DTOLayer;
using ServiceStack.OrmLite;
using System.Threading.Tasks;
using Dapper;
using ServiceStack.Text;

namespace DataLayer
{
    using DataLayer.Extensions;

    using PagedList;

    public class ProcessoRepository : ARepository, IProcessoRepository
    {
        #region CRUD
        public List<Processo> GetAll(int pageNumber, int pageSize, out int count)
        {
            try
            {
                var connection = ConnectionOrmLite.OpenConnection();

                count = (int)connection.Count<Processo>();
                return connection.Select<Processo>(x => x.Page(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Processo!", ex.InnerException);
            }
        }

        public async Task<List<Processo>> GetAll()
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().LoadSelectAsync<Processo>(x => x);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Processo!", ex.InnerException);
            }
        }

        public async Task<Processo> FindById(int id)
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().SingleByIdAsync<Processo>(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível procurar o Id '{id}' na tabela de PROCESSO!", ex.InnerException);
            }
        }

        public Processo Add(Processo processo)
        {
            try
            {
                var connection = ConnectionOrmLite.OpenConnection();
                connection.SetCommandTimeout(120);
                processo.Id = (int)connection.Insert(processo, true);

                //processo.Id = (int)ConnectionOrmLite.AbreConexao().Insert(processo, true);
                return processo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível iserir o Processo '{processo.Numero}' na tabela de PROCESSO!", ex.InnerException);
            }
        }

        public Processo Update(Processo processo)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(processo);
                return processo;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Não foi possível atualizar o Processo '{processo.Numero}' na tabela de PROCESSO!", ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<ProcessoImported>(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível remover da tabela de PROCESSO o Id '{id}'!", ex.InnerException);
            }
        }

        #endregion CRUD
        
        public async Task<Processo> FindByNumeroProcesso(string numero)
        {
            try
            {
                var result = await ConnectionOrmLite.OpenConnection().LoadSelectAsync<Processo>(x => x.Numero == numero);

                return result.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível procurar o Processo '{numero}' na tabela de PROCESSO!", ex.InnerException);
            }
        }

        public async Task<Processo> GetAllOfProcessoByNumeroAsync(string numero)
        {
            try
            {
                var result = await ConnectionOrmLite.OpenConnection().LoadSelectAsync<Processo>(p => p.Numero == numero);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível procurar o Processo '{numero}' na tabela de PROCESSO!", ex.InnerException);
            }

        }

        public void BulkInsertOrUpdate(DataTable modelDataTable, SqlTransaction transaction)
        {
            try
            {
                using (var cmd = new SqlCommand("UPDATE_PROCESSO"))
                {
                    var connection = ConnectionDapper.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableProcessos", modelDataTable);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível inserir os Processos! " + ex.Message);
            }
        }
    }
}
