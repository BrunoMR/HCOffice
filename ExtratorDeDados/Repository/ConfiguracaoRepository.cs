using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using ExtratorDeDados.Models;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class ConfiguracaoRepository : ARepositorySelect<Configuracao>
    {
        public override List<Configuracao> Buscar(Configuracao model)
        {
            try
            {
                var parameters = FilterParameters(model);
                var query = new StringBuilder();

                query.AppendLine("SELECT ");
                query.AppendLine("*");
                query.AppendLine("FROM");
                query.AppendLine("CONFIGURAO");
                BuildWhere(model, ref query);

                var connection = Conexao.RetornaInstancia();
                var configuracao = connection.AbreConexao().Query<Configuracao>(query.ToString(), parameters);

                return configuracao.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível procurar a Configuração!", ex.InnerException);
            }
        }

        protected override DynamicParameters FilterParameters(Configuracao model)
        {
            var parameters = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(model.Descricao))
                parameters.Add("@DESCRICAO", model.Descricao);
            if (!string.IsNullOrWhiteSpace(model.Valor))
                parameters.Add("@VALOR", model.Valor);
            return parameters;
        }

        protected override void BuildWhere(Configuracao model, ref StringBuilder query)
        {
            if ((!string.IsNullOrWhiteSpace(model.Descricao)) || (!string.IsNullOrWhiteSpace(model.Valor)))
                query.AppendLine("WHERE");

            if (!string.IsNullOrWhiteSpace(model.Descricao))
                query.AppendLine("DESCRICAO = @DESCRICAO");

            if ((!string.IsNullOrWhiteSpace(model.Descricao)) && (!string.IsNullOrWhiteSpace(model.Valor)))
                query.AppendLine("AND");

            if (!string.IsNullOrWhiteSpace(model.Valor))
                query.AppendLine("VALOR = @VALOR");
        }
    }
}
