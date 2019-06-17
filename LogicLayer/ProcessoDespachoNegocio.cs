using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTOLayer;
using ExtratorDeDados.Repository;
using Utils;

namespace ExtratorDeDados.Negocio
{
    public class ProcessoDespachoNegocio
    {
        #region Public Methods

        public static List<ProcessoDespacho> BuildProcessoDespachos(RPI rpi)
        {
            try
            {
                var processoDespachoList = new List<ProcessoDespacho>();

                rpi.Processo.ForEach(pro =>
                {
                    pro.Despachos.Despacho.ForEach(des =>
                    {
                        processoDespachoList.Add(new ProcessoDespacho()
                        {
                            NumeroProcesso = pro.NumeroProcesso,
                            CodigoDespacho = des.Codigo,
                            Complemento = des.Complemento,
                            DataDespacho = rpi.DataRpi,
                            NumeroProtocolo = des.Protocolo?.Numero,
                            NumeroRpi = rpi.NumeroRpi
                        });
                    });
                });

                return processoDespachoList;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível encontrar o Despacho em um Processo da RPI '{0}'", rpi.NumeroRpi), ex.InnerException);
            }

        }

        public static void BulkInsert(List<ProcessoDespacho> processoDespachos, SqlTransaction transaction)
        {
            var processoDespachoRepository = new ProcessoDespachoRepository();
            processoDespachoRepository.BulkInsert(CreateDataTable(processoDespachos), transaction);
        }

        #endregion Public Methods

        #region Private Methods

        private static DataTable CreateDataTable(List<ProcessoDespacho> processoDespachos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO_PROCESSO", typeof(string));
            dataTable.Columns.Add("CODIGO_DESPACHO", typeof(string));
            dataTable.Columns.Add("NUMERO_RPI", typeof(int));
            dataTable.Columns.Add("DATA_DESPACHO", typeof(DateTime));
            dataTable.Columns.Add("NUMERO_PROTOCOLO", typeof(string));
            dataTable.Columns.Add("COMPLEMENTO", typeof(string));

            processoDespachos.ForEach(proDes =>
            {
                dataTable.Rows.Add(proDes.NumeroProcesso,
                    proDes.CodigoDespacho,
                    proDes.NumeroRpi,
                    proDes.DataDespacho.VerificarData(),
                    proDes.NumeroProtocolo,
                    proDes.Complemento);
            });

            return dataTable;
        }

        #endregion Private Methods
    }
}
