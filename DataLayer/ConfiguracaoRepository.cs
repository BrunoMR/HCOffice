using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DataLayer.Connections;
using DTOLayer;

namespace DataLayer
{
    public class ConfiguracaoRepository : ARepository, IConfiguracaoRepository
    {
        public List<Configuracao> Find(Configuracao model)
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

                var connection = ConnectionDapper.RetornaInstancia();
                var configuracao = connection.AbreConexao().Query<Configuracao>(query.ToString(), parameters);

                return configuracao.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível procurar a Configuração!", ex.InnerException);
            }
        }

        public DynamicParameters FilterParameters(Configuracao model)
        {
            var parameters = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(model.Descricao))
                parameters.Add("@DESCRICAO", model.Descricao);
            if (!string.IsNullOrWhiteSpace(model.Valor))
                parameters.Add("@VALOR", model.Valor);
            return parameters;
        }

        public void BuildWhere(Configuracao model, ref StringBuilder query)
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
