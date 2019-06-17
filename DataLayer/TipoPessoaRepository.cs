using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DataLayer.Connections;
using DTOLayer;
using ServiceStack.OrmLite;

namespace DataLayer
{
    public class TipoPessoaRepository : ARepository, ITipoPessoaRepository
    {
        #region CRUD

        public List<TipoPessoa> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<TipoPessoa>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Tipo Pessoa!", ex.InnerException);
            }
        }

        public TipoPessoa FindByTipo(char tipo)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Single<TipoPessoa>(x => x.Tipo == tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Tipo Pessoa com ID = '{0}'!", tipo), ex.InnerException);
            }
        }

        public TipoPessoa Add(TipoPessoa tipoPessoa)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Insert(tipoPessoa, true);
                return tipoPessoa;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir o Tipo Pessoa '{0}'!", tipoPessoa.Descricao), ex.InnerException);
            }
        }

        public TipoPessoa Update(TipoPessoa tipoPessoa)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(tipoPessoa);
                return tipoPessoa;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar o Tipo Pessoa '{0}'!", tipoPessoa.Descricao), ex.InnerException);
            }
        }

        public void Remove(char tipo)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Delete<TipoPessoa>(x => x.Tipo == tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível excluir a Tipo Pessoa com ID = '{0}'!", tipo), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
