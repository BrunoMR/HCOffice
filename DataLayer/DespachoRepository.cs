namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;

    public class DespachoRepository : ARepository, IDespachoRepository
    {
        #region CRUD

        public List<Despacho> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<Despacho>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de DESPACHO!", ex.InnerException);
            }
        }

        public Despacho FindById(int id)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().LoadSingleById<Despacho>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Id '{0}' na tabela de DESPACHO!", id), ex.InnerException);
            }
        }

        public Despacho Add(Despacho despacho)
        {
            try
            {
                despacho.Id = (int)ConnectionOrmLite.OpenConnection().Insert(despacho, true);
                return despacho;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível iserir o Despacho '{0}' na tabela de DESPACHO!", despacho.Codigo), ex.InnerException);
            }
        }

        public Despacho Update(Despacho despacho)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(despacho);
                return despacho;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar o Despacho '{0}' na tabela de DESPACHO!", despacho.Codigo), ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<Despacho>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível remover da tabela de DESPACHO o Id '{0}'!", id), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
