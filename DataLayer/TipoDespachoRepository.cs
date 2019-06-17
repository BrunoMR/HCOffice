namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;

    public class TipoDespachoRepository : ARepository, ITipoDespachoRepository
    {
        #region CRUD
        public List<TipoDespacho> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<TipoDespacho>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Tipo Despacho!", ex.InnerException);
            }
        }

        public TipoDespacho FindByTipo(char tipo)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Single<TipoDespacho>(td => td.Tipo == tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Tipo Despacho com ID = '{0}'!", tipo), ex.InnerException);
            }
        }

        public TipoDespacho Add(TipoDespacho tipoDespacho)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Insert(tipoDespacho, true);
                return tipoDespacho;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir o Tipo Despacho '{0}'!", tipoDespacho.Descricao), ex.InnerException);
            }
        }

        public TipoDespacho Update(TipoDespacho tipoDespacho)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(tipoDespacho);
                return tipoDespacho;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar o Tipo Despacho '{0}'!", tipoDespacho.Descricao), ex.InnerException);
            }
        }

        public void Remove(char tipo)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Delete<TipoDespacho>(td => td.Tipo == tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível excluir a Tipo Despacho com ID = '{0}'!", tipo), ex.InnerException);
            }
        }
        #endregion CRUD
    }
}
