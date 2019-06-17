using System.Threading.Tasks;

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using DataLayer;
    using DTOLayer;
    using Utils;

    public class ProcessoDespachoNegocio : IProcessoDespachoNegocio
    {
        #region Public Methods

        /// <summary>
        /// Método irá fazer a ligação dos despachos com o processo para salvar no banco de dados
        /// </summary>
        /// <param name="rpi"></param>
        /// <returns></returns>
        public static List<ProcessoDespachoImported> BuildProcessoDespachos(RpiImported rpi)
        {
            try
            {
                var processoDespachoList = new List<ProcessoDespachoImported>();

                rpi.Processo.ForEach(pro =>
                {
                    LogProcess.PutCurrentProcess(pro);
                    pro.Despachos.Despacho.ForEach(des =>
                    {
                        processoDespachoList.Add(new ProcessoDespachoImported()
                        {
                            NumeroProcesso = pro.NumeroProcesso,
                            CodigoDespacho = des.Codigo,
                            Complemento = des.Complemento,
                            DataDespacho = rpi.DataRpi,
                            NumeroProtocolo = des.Protocolo?.Numero,
                            NumeroRpi = rpi.NumeroRpi
                        });
                    });
                    LogProcess.PutLastProcess(pro);
                });

                return processoDespachoList;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível encontrar o Despacho no Processo '{0}' da RPI '{1}' !", LogProcess.CurrentProcesso.NumeroProcesso, rpi.NumeroRpi), ex.InnerException);
            }

        }

        public static void BulkInsert(List<ProcessoDespachoImported> processoDespachos, SqlTransaction transaction)
        {
            IProcessoDespachoRepository processoDespachoRepository = new ProcessoDespachoRepository();
            processoDespachoRepository.BulkInsert(CreateDataTable(processoDespachos), transaction);
        }

        public async Task<List<ProcessoDespacho>> ProcessoDespachoLoadByProcessoIdAsync(int processoId)
        {
            try
            {
                IProcessoDespachoRepository processoDespachoRepository = new ProcessoDespachoRepository();

                return await processoDespachoRepository.ProcessoDespachoLoadByProcessoIdAsync(processoId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static DataTable CreateDataTable(List<ProcessoDespachoImported> processoDespachos)
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
