namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;

    public class TipoSituacaoRepository : ARepository, ITipoSituacaoRepository
    {
        #region CRUD

        public List<TipoSituacao> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<TipoSituacao>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Tipo Situação!", ex.InnerException);
            }
        }

        public TipoSituacao FindById(int id)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().SingleById<TipoSituacao>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Tipo Situação com ID = '{0}'!", id), ex.InnerException);
            }
        }

        public TipoSituacao Add(TipoSituacao tipoSituacao)
        {
            try
            {
                tipoSituacao.Tipo = (int)ConnectionOrmLite.OpenConnection().Insert(tipoSituacao, true);
                return tipoSituacao;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir o Tipo Situação '{0}'!", tipoSituacao.Descricao), ex.InnerException);
            }
        }

        public TipoSituacao Update(TipoSituacao tipoSituacao)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(tipoSituacao);
                return tipoSituacao;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar o Tipo Situação '{0}'!", tipoSituacao.Descricao), ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<TipoSituacao>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível excluir a Tipo Situação com ID = '{0}'!", id), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
