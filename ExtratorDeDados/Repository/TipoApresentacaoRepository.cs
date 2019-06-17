using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using ExtratorDeDados.Models;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class TipoApresentacaoRepository : ARepositorySelect<TipoApresentacao>
    {
        public override List<TipoApresentacao> Buscar(TipoApresentacao model)
        {
            try
            {
                var parameters = FilterParameters(model);
                var query = new StringBuilder();

                query.AppendLine("SELECT ");
                query.AppendLine("*");
                query.AppendLine("FROM");
                query.AppendLine("TIPO_APRESENTACAO");
                BuildWhere(model, ref query);

                var connection = Conexao.RetornaInstancia();
                var tipoApresentacao = connection.AbreConexao().Query<TipoApresentacao>(query.ToString(), parameters);

                return tipoApresentacao.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível procurar o Tipo de Apresentação!", ex.InnerException);
            }
        }

        protected override void BuildWhere(TipoApresentacao model, ref StringBuilder query)
        {
            if ((model.Tipo > 0) || (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("WHERE");

            if (model.Tipo > 0)
                query.AppendLine("TIPO = @TIPO");

            if ((model.Tipo > 0) && (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("AND");

            if (!string.IsNullOrWhiteSpace(model.Descricao))
                query.AppendLine("DESCRICAO = @DESCRICAO");
        }

        protected override DynamicParameters FilterParameters(TipoApresentacao model)
        {
            var parameters = new DynamicParameters();
            if (model.Tipo > 0)
                parameters.Add("@TIPO", model.Tipo);
            if (!string.IsNullOrWhiteSpace(model.Descricao))
                parameters.Add("@DESCRICAO", model.Descricao);
            return parameters;
        }
    }
}
