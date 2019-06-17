namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using DataLayer;
    using DTOLayer;
    using Utils;

    public class ProtocoloNegocio
    {
        public static void BulkInsert(List<ProcessoImported> model, SqlTransaction transaction)
        {
            var protocoloRepository = new ProtocoloRepository();
            protocoloRepository.BulkInsert(CreateDataTable(model), transaction);
        }
        public static void AddProtocolo(ProtocoloImported model, SqlTransaction transaction)
        {
            ProtocoloRepository protocoloRepository = new ProtocoloRepository();
            protocoloRepository.AddProtocolo(model, transaction);
        }

        private static DataTable CreateDataTable(List<ProcessoImported> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO", typeof(string));
            dataTable.Columns.Add("DATA", typeof(DateTime));
            dataTable.Columns.Add("CODIGO_SERVICO", typeof(string));
            dataTable.Columns.Add("NOME_RAZAO_SOCIAL", typeof(string));
            dataTable.Columns.Add("PAIS", typeof(string));
            dataTable.Columns.Add("UF", typeof(string));

            //int inicial = 660;
            //int final = 17;

            //processos = processos.GetRange(inicial, final).ToList();

            processos.ForEach(pro =>
            {
                pro.Despachos?.Despacho
                .Where(des => des.Protocolo != null)
                .ToList()
                .ForEach(des =>
                {
                    var existsProcotolo =
                        dataTable.AsEnumerable().Any(x => x.Field<string>("NUMERO") == des.Protocolo.Numero);
                    if (!existsProcotolo)
                    {
                        dataTable.Rows.Add(des.Protocolo.Numero,
                            des.Protocolo.Data.VerificarData(),
                            des.Protocolo.CodigoServico,
                            des.Protocolo.Requerente?.Nome,
                            des.Protocolo.Requerente?.Pais,
                            des.Protocolo.Requerente?.Uf);
                    }
                });
            });
            
            

            return dataTable;
        }

    }
}
