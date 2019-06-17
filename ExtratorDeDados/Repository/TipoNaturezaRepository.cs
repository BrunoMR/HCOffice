using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using ExtratorDeDados.Models;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class TipoNaturezaRepository : ARepositorySelect<TipoNatureza>
    {
        public override List<TipoNatureza> Buscar(TipoNatureza model)
        {
            try
            {
                var parameters = FilterParameters(model);
                var query = new StringBuilder();

                query.AppendLine("SELECT ");
                query.AppendLine("*");
                query.AppendLine("FROM");
                query.AppendLine("TIPO_NATUREZA");
                BuildWhere(model, ref query);

                var connection = Conexao.RetornaInstancia();
                var tipoNatureza = connection.AbreConexao().Query<TipoNatureza>(query.ToString(), parameters);

                return tipoNatureza.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível procurar o Tipo de Apresentação!", ex.InnerException);
            }
        }

        protected override void BuildWhere(TipoNatureza model, ref StringBuilder query)
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

        protected override DynamicParameters FilterParameters(TipoNatureza model)
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
