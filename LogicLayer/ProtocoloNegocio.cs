using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DTOLayer;
using ExtratorDeDados.Repository;
using Utils;

namespace ExtratorDeDados.Negocio
{
    public class ProtocoloNegocio
    {
        public static void BulkInsert(List<Processo> model, SqlTransaction transaction)
        {
            var protocoloRepository = new ProtocoloRepository();
            protocoloRepository.BulkInsert(CreateDataTable(model), transaction);
        }
        public static void AddProtocolo(Protocolo model, SqlTransaction transaction)
        {
            ProtocoloRepository protocoloRepository = new ProtocoloRepository();
            protocoloRepository.AddProtocolo(model, transaction);
        }

        private static DataTable CreateDataTable(List<Processo> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO", typeof(string));
            dataTable.Columns.Add("DATA", typeof(DateTime));
            dataTable.Columns.Add("CODIGO_SERVICO", typeof(string));
            dataTable.Columns.Add("NOME_RAZAO_SOCIAL", typeof(string));
            dataTable.Columns.Add("PAIS", typeof(string));
            dataTable.Columns.Add("UF", typeof(string));

            processos.ForEach(pro =>
            {
                pro.Despachos?.Despacho
                .Where(des => des.Protocolo != null)
                .ToList()
                .ForEach(des =>
                {
                    dataTable.Rows.Add(des.Protocolo.Numero,
                        des.Protocolo.Data.VerificarData(),
                        des.Protocolo.CodigoServico,
                        des.Protocolo.Requerente?.Nome,
                        des.Protocolo.Requerente?.Pais,
                        des.Protocolo.Requerente?.Uf);
                });
            });
            
            return dataTable;
        }

    }
}
