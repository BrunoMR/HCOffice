using System;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using ExtratorDeDados.Models;
using Utils;

namespace ExtratorDeDados.Repository
{
    public class RpiRepository
    {
        public void AddRpi(RPI rpi, SqlTransaction transaction)
        {
            try
            {
                var parameters = DynamicParameters(rpi);
                var query = new StringBuilder();
                query.AppendLine("INSERT INTO RPI");
                query.AppendLine("(");
                query.AppendLine("NUMERO,");
                query.AppendLine("DATA");
                query.AppendLine(")");
                query.AppendLine("VALUES");
                query.AppendLine("(");
                query.AppendLine("@NUMERO,");
                query.AppendLine("@DATA");
                query.AppendLine(")");

                var connection = Conexao.RetornaInstancia();
                connection.AbreConexao().Execute(query.ToString(), parameters, transaction);
                
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir a RPI '{0}' ", rpi.NumeroRpi), ex.InnerException);
            }
        }

        private static DynamicParameters DynamicParameters(RPI rpi)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@NUMERO", rpi.NumeroRpi);
            parameters.Add("@DATA", DateTime.Parse(rpi.DataRpi));
            return parameters;
        }
    }
}
