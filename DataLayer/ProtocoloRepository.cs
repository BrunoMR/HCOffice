using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DataLayer.Connections;
using DTOLayer;

namespace DataLayer
{
    public class ProtocoloRepository
    {
        public void BulkInsert(DataTable modeDataTable, SqlTransaction transaction)
        {
            try
            {
                using (var cmd = new SqlCommand("UPDATE_PROTOCOLO"))
                {
                    var connection = ConnectionDapper.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableProtocolos", modeDataTable);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível inserir os Protocolos", ex.InnerException);
            }
        }
        
        public void AddProtocolo(ProtocoloImported protocolo, SqlTransaction transaction)
        {
            try
            {
                var parameters = DynamicParameters(protocolo);
                var query = new StringBuilder();
                query.AppendLine("INSERT INTO PROTOCOLO");
                query.AppendLine("(");
                query.AppendLine("NUMERO,");
                query.AppendLine("DATA,");
                query.AppendLine("CODIGO_SERVICO,");
                query.AppendLine("NOME_RAZAO_SOCIAL,");
                query.AppendLine("PAIS,");
                query.AppendLine("UF");
                query.AppendLine(")");
                query.AppendLine("VALUES");
                query.AppendLine("(");
                query.AppendLine("@NUMERO,");
                query.AppendLine("@DATA,");
                query.AppendLine("@CODIGO_SERVICO,");
                query.AppendLine("@NOME_RAZAO_SOCIAL,");
                query.AppendLine("@PAIS,");
                query.AppendLine("@UF");
                query.AppendLine(")");

                var connection = ConnectionDapper.RetornaInstancia();
                connection.AbreConexao().Execute(query.ToString(), parameters, transaction);
            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível inserir o Protocolo", ex.InnerException);
            }
        }

        private static DynamicParameters DynamicParameters(ProtocoloImported protocolo)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@NUMERO", protocolo.Numero);
            parameters.Add("@DATA", DateTime.Parse(protocolo.Data));
            parameters.Add("@CODIGO_SERVICO", protocolo.CodigoServico);
            parameters.Add("@NOME_RAZAO_SOCIAL", protocolo.Requerente?.Nome);
            parameters.Add("@PAIS", protocolo.Requerente?.Pais);
            parameters.Add("@UF", protocolo.Requerente?.Uf);
            return parameters;
        }
    }
}
