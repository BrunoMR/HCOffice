using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using ExtratorDeDados.Models;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class DespachoRepository : ARepositorySelect<Despacho>, IRepositoryInsert<Despacho>
    {
        public void Adicionar(Despacho model)
        {
            try
            {
                var parameters = DynamicParameters(model);
                var query = new StringBuilder();
                query.AppendLine("INSERT INTO DESPACHO");
                query.AppendLine("(");
                query.AppendLine("CODIGO,");
                query.AppendLine("DESCRICAO,");
                query.AppendLine(")");
                query.AppendLine("VALUES");
                query.AppendLine("(");
                query.AppendLine("@CODIGO,");
                query.AppendLine("@DESCRICAO,");
                query.AppendLine(")");

                var connection = Conexao.RetornaInstancia();
                connection.AbreConexao().Execute(query.ToString(), parameters);
                connection.FechaConexao();
                
            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível inserir o Protocolo", ex.InnerException);
            }
        }

        public override List<Despacho> Buscar(Despacho model)
        {
            try
            {
                var parameters = FilterParameters(model);
                var query = new StringBuilder();

                query.AppendLine("SELECT ");
                query.AppendLine("*");
                query.AppendLine("FROM");
                query.AppendLine("DESPACHO");
                BuildWhere(model, ref query);

                var connection = Conexao.RetornaInstancia();
                var despachos = connection.AbreConexao().Query<Despacho>(query.ToString(), parameters);
                //connection.FechaConexao();

                return despachos.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível buscar o Despacho!", ex.InnerException);
            }
        }

        protected override void BuildWhere(Despacho model, ref StringBuilder query)
        {
            if ((!string.IsNullOrWhiteSpace(model.Codigo)) || (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("WHERE");

            if (!string.IsNullOrWhiteSpace(model.Codigo))
                query.AppendLine("CODIGO = @CODIGO");

            if ((!string.IsNullOrWhiteSpace(model.Codigo)) && (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("AND");

            if (!string.IsNullOrWhiteSpace(model.Descricao))
                query.AppendLine("DESCRICAO = @DESCRICAO");
        }

        protected override DynamicParameters FilterParameters(Despacho model)
        {
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(model.Codigo))
                parameters.Add("@CODIGO", model.Codigo);
            if (!string.IsNullOrWhiteSpace(model.Descricao))
                parameters.Add("@DESCRICAO", model.Descricao);

            return parameters;
        }

        public DynamicParameters DynamicParameters(Despacho model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CODIGO", model.Codigo);
            parameters.Add("@DESCRICAO", model.Descricao);
            return parameters;
        }
    }
}
