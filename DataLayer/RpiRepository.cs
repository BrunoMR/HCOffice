namespace DataLayer
{
    using System;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Text;
    using Dapper;
    using Connections;
    using DTOLayer;
    using System.Collections.Generic;
    using ServiceStack.OrmLite;

    public class RpiRepository : ARepository, IRpiRepository
    {
        public void Add(RpiImported rpi, SqlTransaction transaction)
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

                var connection = ConnectionDapper.RetornaInstancia();
                connection.AbreConexao().Execute(query.ToString(), parameters, transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível inserir a RPI '{rpi.NumeroRpi}' ", ex.InnerException);
            }
        }

        public DynamicParameters DynamicParameters(RpiImported rpi)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@NUMERO", rpi.NumeroRpi);
            parameters.Add("@DATA", DateTime.Parse(rpi.DataRpi, CultureInfo.CreateSpecificCulture("pt-BR"), DateTimeStyles.AssumeLocal));
            return parameters;
        }

        public List<Rpi> GetAll()
        {
            return ConnectionOrmLite.OpenConnection().Select<Rpi>();
        }
    }
}
