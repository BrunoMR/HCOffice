using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DTOLayer;
using ExtratorDeDados.Repository;
using Utils;

namespace ExtratorDeDados.Negocio
{
    public class ProcessoNegocio
    {
        private static readonly List<int> Despachos400 = new List<int>(new int[] { 400, 401, 403, 404, 405, 406, 451, 453 });
        private static readonly List<int> Despachos900 = new List<int>(new int[] { 990, 991, 992, 993, 994, 995, 996, 997, 998 });

        #region Public Methods

        public static void InsertOrUpdate(List<Processo> processos, SqlTransaction transaction)
        {
            var processoRepository = new ProcessoRepository();
            processoRepository.InsertOrUpdate(CreateDataTable(processos), transaction);
        }

        /// <summary>
        ///  Valida as datas do Processo
        /// </summary>
        /// <param name="processoMatch"></param>
        /// <param name="dataVigenciaProcesso"></param>
        /// <param name="dataDepositoProcesso"></param>
        /// <returns></returns>
        public static DateTime ValidateDatesProcesso(Match processoMatch, out DateTime dataVigenciaProcesso,
            out DateTime dataDepositoProcesso)
        {
            try
            {
                var dataConcessaoProcesso = new DateTime();
                dataVigenciaProcesso = new DateTime();
                dataDepositoProcesso = new DateTime();

                var despachoCode = processoMatch.Groups[3].Value;

                if (Despachos400.Contains(int.Parse(despachoCode)))
                {
                    dataDepositoProcesso = DateTime.Parse(processoMatch.Groups[2].Value);
                    dataConcessaoProcesso = DateTime.Parse(RpiNegocio.CurrentRpi.DataRpi);
                    dataVigenciaProcesso = dataConcessaoProcesso.AddYears(10);
                }
                else if (int.Parse(despachoCode) > 400 && int.Parse(despachoCode) < 990)
                {
                    dataConcessaoProcesso = string.IsNullOrWhiteSpace(processoMatch.Groups[2].Value)
                        ? DateTime.MinValue
                        : DateTime.Parse(processoMatch.Groups[2].Value);
                    dataVigenciaProcesso = dataConcessaoProcesso.AddYears(10);
                }
                else if (Despachos900.Contains(int.Parse(despachoCode)))
                {
                    dataVigenciaProcesso = string.IsNullOrWhiteSpace(processoMatch.Groups[2].Value)
                        ? DateTime.MinValue
                        : DateTime.Parse(processoMatch.Groups[2].Value);
                    dataVigenciaProcesso = dataVigenciaProcesso.AddYears(10);
                }
                else
                {
                    dataDepositoProcesso = string.IsNullOrWhiteSpace(processoMatch.Groups[2].Value)
                        ? DateTime.MinValue
                        : DateTime.Parse(processoMatch.Groups[2].Value);
                }
                return dataConcessaoProcesso;
            }
            catch (Exception ex)
            {
                throw new Exception("Problema ao converter Data!", ex.InnerException);
            }
        }

        #endregion Public Methods

        #region Private Methods
        private static DataTable CreateDataTable(List<Processo> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO", typeof(string));
            dataTable.Columns.Add("NOME_TITULAR", typeof(string));
            dataTable.Columns.Add("CPF_CNPJ_INPI_TITULAR", typeof(string));
            dataTable.Columns.Add("NOME_PROCURADOR", typeof(string));
            dataTable.Columns.Add("MARCA", typeof(string));
            dataTable.Columns.Add("MARCA_ORTOGRAFADA", typeof(string));
            dataTable.Columns.Add("PRIORIDADE", typeof(string));
            dataTable.Columns.Add("DATA_PRIORIDADE", typeof(DateTime));
            dataTable.Columns.Add("NOME_PAIS_PRIORIDADE", typeof(string));
            dataTable.Columns.Add("TIPO_APRESENTACAO", typeof(int));
            dataTable.Columns.Add("TIPO_NATUREZA", typeof(int));
            dataTable.Columns.Add("CLASSE_INTERNACIONAL", typeof(string));
            dataTable.Columns.Add("CLASSE_1", typeof(string));
            dataTable.Columns.Add("CLASSE_2", typeof(string));
            dataTable.Columns.Add("CLASSE_3", typeof(string));
            dataTable.Columns.Add("ESPECIFICACAO", typeof(string));
            dataTable.Columns.Add("APOSTILA", typeof(string));
            dataTable.Columns.Add("NUMERO_REFERENCIA", typeof(string));
            dataTable.Columns.Add("DATA_DEPOSITO", typeof(DateTime));
            dataTable.Columns.Add("DATA_CONCESSAO", typeof(DateTime));
            dataTable.Columns.Add("DATA_REGISTRO", typeof(DateTime));
            dataTable.Columns.Add("DATA_VIGENCIA", typeof(DateTime));

            try
            {
                processos.ForEach(pro =>
                {
                    dataTable.Rows.Add(pro.NumeroProcesso,
                        pro.Titulares?.Titular.Nome,
                        pro.Titulares?.Titular.CpfCnpj,
                        pro.Procurador,
                        pro.Marca?.Nome,
                        pro.Marca?.Nome.Ortografar(),
                        pro.Prioridades?.Prioridade.Numero,
                        pro.Prioridades?.Prioridade.Data.VerificarData(),
                        pro.Prioridades?.Prioridade.Pais,
                        TipoApresentacaoNegocio.FindByDescription(pro.Marca?.Apresentacao),
                        TipoNaturezaNegocio.FindByDescription(pro.Marca?.Natureza),
                        ClasseNegocio.RetrieveCodeClasseNiceIfFromXml(pro.ClasseNice?.Codigo, pro.NumeroProcesso),
                        ClasseNegocio.BuildClasseNacionalAndSubClasse(pro, 0),
                        ClasseNegocio.BuildClasseNacionalAndSubClasse(pro, 1),
                        ClasseNegocio.BuildClasseNacionalAndSubClasse(pro, 2),
                        pro.ClasseNacional?.Descricao,
                        pro.Apostila,
                        "Não tem",
                        pro.DataDeposito.VerificarData(),
                        pro.DataConcessao.VerificarData(),
                        pro.DataRegistro.VerificarData(),
                        pro.DataVigencia.VerificarData());
                });

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }

        #endregion Private Methods
    }
}
