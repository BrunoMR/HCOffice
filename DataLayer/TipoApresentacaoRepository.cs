using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Connections;
using DTOLayer;
using ServiceStack.OrmLite;

namespace DataLayer
{
    public class TipoApresentacaoRepository : ARepository, ITipoApresentacaoRepository
    {
        public List<TipoApresentacao> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<TipoApresentacao>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Tipo Apresentação!", ex.InnerException);
            }
        }

        public TipoApresentacao FindById(int id)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().SingleById<TipoApresentacao>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar a Apresentação com ID = '{0}'!", id), ex.InnerException);
            }
        }

        public TipoApresentacao Add(TipoApresentacao tipoApresentacao)
        {
            try
            {
                tipoApresentacao.Id = (int)ConnectionOrmLite.OpenConnection().Insert(tipoApresentacao, true);
                return tipoApresentacao;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir a Apresentação '{0}'!", tipoApresentacao.Descricao), ex.InnerException);
            }
        }

        public TipoApresentacao Update(TipoApresentacao tipoApresentacao)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(tipoApresentacao);
                return tipoApresentacao;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar a Apresentação '{0}'!", tipoApresentacao.Descricao), ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<TipoApresentacao>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível excluir a Apresentação com ID = '{0}'!", id), ex.InnerException);
            }
        }
    }
}
