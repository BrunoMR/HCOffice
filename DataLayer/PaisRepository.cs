namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;
    using System.Threading.Tasks;

    public class PaisRepository : ARepository, IPaisRepository
    {
        #region CRUD

        public List<Pais> GetAllAsync()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<Pais>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de País!", ex.InnerException);
            }
        }

        public async Task<Pais> FindByIdAsync(int id)
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().SingleByIdAsync<Pais>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o País com ID = '{0}'!", id), ex.InnerException);
            }
        }

        public Pais Add(Pais pais)
        {
            try
            {
                pais.Id = (int)ConnectionOrmLite.OpenConnection().Insert(pais, true);
                return pais;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir o País '{0}'!", pais.Sigla), ex.InnerException);
            }
        }

        public Pais Update(Pais pais)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(pais);
                return pais;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar o País '{0}'!", pais.Sigla), ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<Pais>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível excluir o País com ID = '{0}'!", id), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}