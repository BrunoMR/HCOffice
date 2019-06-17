using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using ExtratorDeDados.Models;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class Cfe4Repository : ARepositorySelect<CFE4>
    {
        public void InsertOrUpdate(DataTable dataTableModel, SqlTransaction transaction)
        {
            try
            {
                using (var cmd = new SqlCommand("UPDATE_PROCESSO_CFE4"))
                {
                    var connection = Conexao.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableCfe4s", dataTableModel);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Não foi possível inserir as Classes Vienna ", ex.InnerException);
            }
        }

        public override List<CFE4> Buscar(CFE4 model)
        {
            try
            {
                var parameters = FilterParameters(model);
                var query = new StringBuilder();

                query.AppendLine("SELECT ");
                query.AppendLine("*");
                query.AppendLine("FROM");
                query.AppendLine("CFE4");
                BuildWhere(model, ref query);

                var connection = Conexao.RetornaInstancia();
                var cfe = connection.AbreConexao().Query<CFE4>(query.ToString(), parameters);

                return cfe.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível procurar o CFE4!", ex.InnerException);
            }
        }

        protected override void BuildWhere(CFE4 model, ref StringBuilder query)
        {
            if ((!string.IsNullOrWhiteSpace(model.Codigo_CFE4)) || (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("WHERE");

            if (!string.IsNullOrWhiteSpace(model.Codigo_CFE4))
                query.AppendLine("CODIGO_CFE4 = @CODIGO_CFE4");

            if ((!string.IsNullOrWhiteSpace(model.Codigo_CFE4)) && (!string.IsNullOrWhiteSpace(model.Descricao)))
                query.AppendLine("AND");

            if (!string.IsNullOrWhiteSpace(model.Descricao))
                query.AppendLine("DESCRICAO = @DESCRICAO");
        }

        protected override DynamicParameters FilterParameters(CFE4 model)
        {
            var parameters = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(model.Codigo_CFE4))
                parameters.Add("@CODIGO_CFE4", model.Codigo_CFE4);
            if (!string.IsNullOrWhiteSpace(model.Descricao))
                parameters.Add("@DESCRICAO", model.Descricao);
            return parameters;
        }
        
        
    }
}
