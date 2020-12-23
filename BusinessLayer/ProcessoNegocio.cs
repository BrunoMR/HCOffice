namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using DataLayer;
    using DTOLayer;
    using Utils;

    /// <summary>
    /// The processo negocio.
    /// </summary>
    public class ProcessoNegocio : IProcessoNegocio
    {
        /// <summary>
        /// The despachos 400.
        /// </summary>
        private static readonly List<int> Despachos400 = new List<int>(new[] { 400, 401, 403, 404, 405, 406, 451, 453 });

        /// <summary>
        /// The despachos 900.
        /// </summary>
        private static readonly List<int> Despachos900 = new List<int>(new[] { 990, 991, 992, 993, 994, 995, 996, 997, 998 });

        /// <summary>
        /// The requerente attributes.
        /// </summary>
        private static readonly List<string> RequerenteAttributes = new List<string>
        {
            "3481",
            "3482",
            "3483",
            "P09",
            "P11"
        };

        /// <summary>
        /// The procurador attributes.
        /// </summary>
        private static readonly List<string> ProcuradorAttributes = new List<string>
        {
            "36323",
            "36324",
            "36325",
            "3633",
            "3851",
            "3852"
        };

        readonly IProcessoRepository _processoRepository = new ProcessoRepository();

        #region CRUD

        public List<Processo> GetAll(int pageNumber, int pageSize, out int count)
        {
            try
            {
                return _processoRepository.GetAll(pageNumber, pageSize, out count);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<Processo> FindById(int id)
        {
            try
            {
                return await _processoRepository.FindById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Processo AddOrUpdate(Processo processo)
        {
            return processo?.Id == null
                ? Add(processo)
                : Update(processo);
        }

        private Processo Add(Processo processo)
        {
            try
            {
                return _processoRepository.Add(processo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Processo Update(Processo processo)
        {
            try
            {
                return _processoRepository.Update(processo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion CRUD

        #region Public Methods

        public async Task<Processo> FindByNumeroProcesso(string numero)
        {
            try
            {
                return await _processoRepository.FindByNumeroProcesso(numero);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static void BulkInsertOrUpdate(List<ProcessoImported> processos, SqlTransaction transaction)
        {
            IProcessoRepository processoRepository = new ProcessoRepository();
            processoRepository.BulkInsertOrUpdate(CreateDataTable(processos), transaction);
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
                    dataDepositoProcesso = DateTime.Parse(processoMatch.Groups[2].Value,
                        CultureInfo.CreateSpecificCulture("pt-BR"), DateTimeStyles.AssumeLocal);
                    dataConcessaoProcesso = DateTime.Parse(RpiNegocio.CurrentRpi.DataRpi,
                        CultureInfo.CreateSpecificCulture("pt-BR"), DateTimeStyles.AssumeLocal);
                    dataVigenciaProcesso = dataConcessaoProcesso.AddYears(10);
                }
                else if (int.Parse(despachoCode) > 400 && int.Parse(despachoCode) < 990)
                {
                    dataConcessaoProcesso = string.IsNullOrWhiteSpace(processoMatch.Groups[2].Value)
                        ? DateTime.MinValue
                        : DateTime.Parse(processoMatch.Groups[2].Value, CultureInfo.CreateSpecificCulture("pt-BR"),
                            DateTimeStyles.AssumeLocal);
                    dataVigenciaProcesso = dataConcessaoProcesso.AddYears(10);
                }
                else if (Despachos900.Contains(int.Parse(despachoCode)))
                {
                    dataVigenciaProcesso = string.IsNullOrWhiteSpace(processoMatch.Groups[2].Value)
                        ? DateTime.MinValue
                        : DateTime.Parse(processoMatch.Groups[2].Value, CultureInfo.CreateSpecificCulture("pt-BR"),
                            DateTimeStyles.AssumeLocal);
                    dataVigenciaProcesso = dataVigenciaProcesso.AddYears(10);
                }
                else
                {
                    dataDepositoProcesso = string.IsNullOrWhiteSpace(processoMatch.Groups[2].Value)
                        ? DateTime.MinValue
                        : DateTime.Parse(processoMatch.Groups[2].Value, CultureInfo.CreateSpecificCulture("pt-BR"),
                            DateTimeStyles.AssumeLocal);
                }
                return dataConcessaoProcesso;
            }
            catch (Exception ex)
            {
                throw new Exception("Problema ao converter Data!", ex.InnerException);
            }
        }

        public async Task<List<Processo>> GetAllFullProcessoAsync()
        {
            var processos = await _processoRepository.GetAll();

            processos.ForEach(async p =>
            {
                await LoadProcessoDespachoByProcesso(p);
                await LoadProcessoCfe4ByProcesso(p);
            });

            return processos;
        }

        public async Task<Processo> GetAllOfProcessoByNumeroAsync(string numero)
        {
            var processo = await _processoRepository.GetAllOfProcessoByNumeroAsync(numero);

            await LoadProcessoDespachoByProcesso(processo);
            await LoadProcessoCfe4ByProcesso(processo);

            return processo;
        }

        public async Task<List<Processo>> GetAllFullProcessoAsyncTeste()
        {
            var processos = await _processoRepository.GetAll();

            //foreach (var pro in processos)
            //{
            //    pro.ProcessoCfe4List.Clear();
            //    pro.ProcessoCfe4List.AddRange(await LoadProcessoCfe4ByProcesso(pro));
            //}

            processos.ForEach(async p =>
            {
                await LoadProcessoCfe4ByProcesso(p);
                await LoadProcessoDespachoByProcesso(p);
            });

            return processos;
        }

        #endregion Public Methods

        #region Private Methods

        private async Task LoadProcessoDespachoByProcesso(Processo processo)
        {
            IProcessoDespachoNegocio processoDespachoNegocio = new ProcessoDespachoNegocio();
            processo.ProcessoDespachoList?.Clear();
            processo.ProcessoDespachoList?.AddRange(
                await processoDespachoNegocio.ProcessoDespachoLoadByProcessoIdAsync(Convert.ToInt32(processo.Id)));
        }

        private async Task LoadProcessoCfe4ByProcesso(Processo processo)
        {
            if (processo.ProcessoCfe4List != null && processo.ProcessoCfe4List.Count > 0)
            {
                IProcessoCfe4Negocio processoCfe4Negocio = new ProcessoCfe4Negocio();
                var processoCfe4List =
                    await processoCfe4Negocio.ProcessoCfe4LoadByProcessoIdAsync(Convert.ToInt32(processo.Id));

                processo.ProcessoCfe4List?.Clear();
                //processo.ProcessoCfe4List = new List<ProcessoCfe4>();
                processo.ProcessoCfe4List.AddRange(processoCfe4List);
            }
        }

        private static DataTable CreateDataTable(List<ProcessoImported> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO", typeof(string));
            dataTable.Columns.Add("NOME_TITULAR", typeof(string));
            dataTable.Columns.Add("CPF_CNPJ_INPI_TITULAR", typeof(string));
            dataTable.Columns.Add("PAIS_TITULAR", typeof(string));
            dataTable.Columns.Add("UF_TITULAR", typeof(string));
            dataTable.Columns.Add("NOME_PROCURADOR", typeof(string));
            dataTable.Columns.Add("MARCA", typeof(string));
            dataTable.Columns.Add("MARCA_ORTOGRAFADA", typeof(string));
            dataTable.Columns.Add("MARCA_NAO_ORTOGRAFADA", typeof(string));
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
            dataTable.Columns.Add("MARCA_SEM_VOGAIS", typeof(string));
            dataTable.Columns.Add("NUMERO_INSCRICAO_INTERNACIONAL", typeof(string));
            dataTable.Columns.Add("DATA_RECEBIMENTO_INPI", typeof(DateTime));
            dataTable.Columns.Add("TIPO_NATUREZA_DESCRICAO", typeof(string));

            try
            {
                //var inicial = 0;
                //var count = 10;

                processos
                    //.GetRange(inicial, count)
                    .ForEach(pro =>
                    {
                        dataTable.Rows.Add(
                            pro.NumeroProcesso,
                            ReturnTitularName(pro),
                            pro.Titulares?.Titular?.First().CpfCnpj,
                            pro.Titulares?.Titular?.First().Pais,
                            pro.Titulares?.Titular?.First().Uf,
                            ReturnProcuradorName(pro),
                            pro.Marca?.Nome,
                            pro.Marca?.Nome?.Spell(),
                            pro.Marca?.Nome.NotSpellManyWords(),
                            pro.Prioridades?.Prioridade?.Numero,
                            pro.Prioridades?.Prioridade?.Data.VerificarData(),
                            pro.Prioridades?.Prioridade?.Pais,
                            TipoApresentacaoNegocio.FindByDescription(pro.Marca?.Apresentacao),
                            TipoNaturezaNegocio.FindByDescription(pro.Marca?.Natureza),
                            ClasseNegocio.RetrieveCodeClasseNiceIfFromXml(pro.ClasseNice?.Codigo, pro.NumeroProcesso),
                            ClasseNegocio.BuildClasseNacionalAndSubClasse(pro, 0),
                            ClasseNegocio.BuildClasseNacionalAndSubClasse(pro, 1),
                            ClasseNegocio.BuildClasseNacionalAndSubClasse(pro, 2),
                            pro.ClasseNice?.Descricao ?? pro.ClasseNacional?.Descricao,
                            pro.Apostila,
                            "Não tem",
                            pro.DataDeposito.VerificarData(),
                            pro.DataConcessao.VerificarData(),
                            pro.DataRegistro.VerificarData(),
                            pro.DataVigencia.VerificarData(),
                            pro.Marca?.Nome.RetirarVogais(),
                            pro.DadosDeMadri?.NumeroInscricaoInternacional,
                            pro.DadosDeMadri?.DataRecebimentoInpi.VerificarData(),
                            pro.Marca?.Natureza);
                    });


                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string ReturnTitularName(ProcessoImported pro)
        {
            string titularNome = null;

            if (pro.Despachos.Despacho.Any(x => x.Protocolo?.Cessionario != null))
                titularNome = pro.Despachos.Despacho
                    .First(x => x.Protocolo?.Cessionario != null).Protocolo.Cessionario.NomeRazaoSocial;
            else
            {
                titularNome = pro.Titulares?.Titular?.First().Nome;

                if (pro.Despachos.Despacho.Any(x => RequerenteAttributes.Contains(x.Protocolo?.CodigoServico)))
                    titularNome = pro.Despachos.Despacho
                        .First(x => RequerenteAttributes.Contains(x.Protocolo?.CodigoServico)).Protocolo.Requerente.Nome;
            }

            return titularNome;
        }

        private static string ReturnProcuradorName(ProcessoImported pro)
        {
            try
            {
                string procudadorName = null;

                if (pro.Despachos.Despacho.Any(
                        x => ProcuradorAttributes.Contains(x.Protocolo?.CodigoServico)))
                    procudadorName = pro.Despachos.Despacho
                        .First(x => ProcuradorAttributes.Contains(x.Protocolo?.CodigoServico))
                        .Protocolo.Procurador;

                return procudadorName ?? pro.Procurador;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Private Methods
    }
}
