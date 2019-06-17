using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using ExtratorDeDados.Models;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class ClasseRepository : ARepositorySelect<Classe>
    {
        public override List<Classe> Buscar(Classe model)
        {
            try
            {
                var parameters = FilterParameters(model);
                var query = new StringBuilder();

                query.AppendLine("SELECT ");
                query.AppendLine("*");
                query.AppendLine("FROM");
                query.AppendLine("CLASSE");
                BuildWhere(model, ref query);

                var connection = Conexao.RetornaInstancia();
                var classe = connection.AbreConexao().Query<Classe>(query.ToString(), parameters);

                return classe.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível procurar a Classe!", ex.InnerException);
            }
        }

        protected override void BuildWhere(Classe model, ref StringBuilder query)
        {
            if ((!string.IsNullOrWhiteSpace(model.Numero_Classe)) || (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("WHERE");

            if (!string.IsNullOrWhiteSpace(model.Numero_Classe))
                query.AppendLine("NUMERO_CLASSE = @NUMERO_CLASSE");

            if ((!string.IsNullOrWhiteSpace(model.Numero_Classe)) && (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("AND");

            if (!string.IsNullOrWhiteSpace(model.Descricao))
                query.AppendLine("DESCRICAO = @DESCRICAO");
        }

        protected override DynamicParameters FilterParameters(Classe model)
        {
            var parameters = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(model.Numero_Classe))
                parameters.Add("@NUMERO_CLASSE", model.Numero_Classe);
            if (!string.IsNullOrWhiteSpace(model.Descricao))
                parameters.Add("@DESCRICAO", model.Descricao);
            return parameters;
        }
    }
}
