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
    public class TipoNaturezaRepository : ARepository, ITipoNaturezaRepository
    {
        #region CRUD

        public List<TipoNatureza> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<TipoNatureza>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Tipo Natureza!", ex.InnerException);
            }
        }

        public TipoNatureza FindById(int id)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().SingleById<TipoNatureza>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar a Natureza com ID = '{0}'!", id), ex.InnerException);
            }
        }

        public TipoNatureza Add(TipoNatureza tipoNatureza)
        {
            try
            {
                tipoNatureza.Id = (int)ConnectionOrmLite.OpenConnection().Insert(tipoNatureza, true);
                return tipoNatureza;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir a Natureza '{0}'!", tipoNatureza.Descricao), ex.InnerException);
            }
        }

        public TipoNatureza Update(TipoNatureza tipoNatureza)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(tipoNatureza);
                return tipoNatureza;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar a Natureza '{0}'!", tipoNatureza.Descricao), ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<TipoNatureza>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível excluir a Natureza com ID = '{0}'!", id), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
