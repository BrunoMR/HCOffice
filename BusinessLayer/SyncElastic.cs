namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.ServiceProcess;
    using System.Threading;
    using DataLayer;
    using DTOLayer;
    using DTOLayer.Indexes;
    using Nest;
    using Utils;

    /// <summary>
    /// The sync elastic.
    /// </summary>
    public class SyncElastic : ISyncElastic
    {
        /// <summary>
        /// The client.
        /// </summary>
        private static ElasticClient client;

        /// <summary>
        /// The sync elastic repository.
        /// </summary>
        private readonly ISyncElasticRepository syncElasticRepository = new SyncElasticRepository();

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the SyncElastic class.
        /// </summary>
        public SyncElastic()
        {
            client = ElasticSearchHelper.GetElasticClient("Main");
        }

        /// <summary>The synchronization of imported processes.</summary>
        /// <param name="rpiImporteds">The rpi importeds.</param>
        /// <returns>Returns if synchronized success</returns>
        public bool SynchronizationOfImportedProcesses(IEnumerable<RpiImported> rpiImporteds)
        {
            var processes = new List<string>();
            rpiImporteds
                .ToList()
                .ForEach(rpi =>
                {
                    processes.AddRange(rpi.Processo.Select(pro => pro.NumeroProcesso).ToList());
                });

            var processosSync = this.syncElasticRepository.GetImportedProcessos(CreateDataTable(processes));
            var processoList = BuildAllObjectsToIndex(processosSync);

            return Index(processoList, "Main");
        }

        /// <summary>The synchronization processes by all rpis.</summary>
        /// <returns>Returns if synchronized success</returns>
        public bool SynchronizationProcessesByAllRpis()
        {
            //TODO: arrumar aqui
            var rpiNegocio = new RpiNegocio(new Cfe4Negocio(new Cfe4Repository()));
            var rpiList = rpiNegocio.GetAll();

            var result = false;
            rpiList.AsParallel()
                .ForAll(x => result = SynchronizationProcessesByRpi(x.Numero, x.Numero));
            //rpiList
            //    .ForEach(x => result = SynchronizationProcessesByRpi(x.Numero, x.Numero));

            return result;
        }

        public bool SynchronizationProcessesByRpi(int startRpi, int endRpi)
        {
            var processosSync = this.syncElasticRepository.GetFullProcessosByRpi(startRpi, endRpi);
            var processoList = BuildAllObjectsToIndex(processosSync);

            return Index(processoList, "Main");
        }

        public bool SynchronizationOfAllProcuradores()
        {
            var procuradorList = this.syncElasticRepository.GetAllProcuradores();
            return Index(procuradorList, "Procurador");
        }

        public bool SynchronizationOfAllTitulares()
        {
            var titularList = this.syncElasticRepository.GetAllTitulares();
            const int range = 150000;
            var start = 0;
            var end = range;
            var result = false;
            var isThereRegisters = true;

            var titularIndices = titularList as IList<TitularIndex> ?? titularList.ToList();
            while (isThereRegisters)
            {
                if ((start + range) > titularIndices.Count)
                {
                    end = titularIndices.Count - start;
                    isThereRegisters = false;
                }

                result = Index(titularIndices.ToList().GetRange(start, end), "Titular");
                start += end;
            }

            return result;
        }

        public bool SynchronizationOfAllClasses()
        {
            var classeList = this.syncElasticRepository.GetAllClasses();
            return Index(classeList, "Classe");
        }

        public bool SynchronizationOfAllCfe4S()
        {
            var cfe4List = this.syncElasticRepository.GetAllCfe4S();
            return Index(cfe4List.Select(x => new Cfe4Index()
            {
                Codigo = x.Codigo.Trim()
            }), "Cfe4");
        }

        public bool SynchronizationOfAllDespachos()
        {
            var despachoList = this.syncElasticRepository.GetAllDespachos();
            return Index(despachoList, "Despacho");
        }

        public bool SynchronizationOfAllClasseAfinidades()
        {
            var classeAfinidadeSyncList = this.syncElasticRepository.GetAllClassSimilarities();

            var classeAfinidadeList = classeAfinidadeSyncList
                                        .GroupBy(x => new
                                        {
                                            x.Classe
                                        })
                                        .Select(x => new ClasseAfinidadeIndex()
                                        {
                                            ClasseA = x.Key.Classe,
                                            ClasseAfinidade = classeAfinidadeSyncList
                                                                        .Where(c => c.Classe == x.Key.Classe)
                                                                        .Select(a => a.ClasseAfinidade)
                                                                        .ToList()

                                        }).ToList();

            return IndexClasseAfinifades(classeAfinidadeList);
        }

        public bool SynchronizationOfAllPresentations()
        {
            var presentationList = this.syncElasticRepository.GetAllPresentations();
            return Index(presentationList, "Apresentacao");
        }

        public bool SynchronizationOfAllCountries()
        {
            var countryList = this.syncElasticRepository.GetAllCountries();
            return Index(countryList, "Pais");
        }

        public bool SynchronizationOfAllStates()
        {
            var stateList = this.syncElasticRepository.GetAllStates();
            return Index(stateList, "Estado");
        }

        public bool SynchronizationOfAllAtrhibutes()
        {
            var stateList = this.syncElasticRepository.GetAllAthributes();
            return Index(stateList, "Atributo");
        }

        public bool SynchronizationOfRpi(int numberRpi)
        {
            throw new NotImplementedException();
        }

        public int GetCountToSync()
        {
            return this.syncElasticRepository.GetCountToSync();
        }

        public void SynchronizationOfAllDatabase()
        {
            const int range = 40000; // 100000;
            const int start = 0;

            var processoIdList = this.syncElasticRepository.GetAllIdsOfProcesso();
            var total = processoIdList.ToList().Count;

            var indexou = false;
            //var tempo = new Stopwatch();
            //tempo.Start();
            for (var i = 0; i < total; i += range)
            {
                var processosSync = this.syncElasticRepository.GetFullProcessos((CreateDataTableOfIdProcesso(processoIdList, start, range)));

                //tempo.Start();
                var processoList = BuildAllObjectsToIndex(processosSync);
                //tempo.Stop();

                indexou = Index(processoList, "Main");

                GC.Collect();
            }
            //tempo.Stop();
            GC.Collect();
        }

        #endregion Public Methods

        #region Private Methods

        private IEnumerable<ProcessoIndex> BuildAllObjectsToIndex(IEnumerable<ProcessoSync> processosSync)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            var processoList = GroupByProcessos(processosSync);
            var cfe4List = GroupByCfe4S(processosSync);
            var despachoList = GroupByDespachos(processosSync);
            var classeList = new List<ClasseSync>();
            classeList.AddRange(GroupByClasseInternacional(processosSync));
            classeList.AddRange(GroupByClasseNacional(processosSync));
            classeList.AddRange(GroupByClasse(processosSync));

            BuildProcessoWithChilds(processoList, cfe4List, despachoList, classeList);

            return processoList;
        }

        private DataTable CreateDataTable(List<string> processos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO_PROCESSO", typeof(string));

            processos.ForEach(pro =>
                        {
                            dataTable.Rows.Add(pro);
                        });

            return dataTable;
        }

        private DataTable CreateDataTableOfIdProcesso(List<ProcessoIds> processoIdList, int start, int count)
        {
            if (processoIdList.Count < count)
                count = processoIdList.ToList().Count;

            var dataTable = new DataTable();
            dataTable.Columns.Add("ID_PROCESSO", typeof(int));

            processoIdList
                .GetRange(start, count)
                .ForEach(pro =>
                {
                    dataTable.Rows.Add(pro.Id);
                });

            processoIdList.RemoveRange(start, count);

            return dataTable;
        }

        private List<ProcessoIndex> GroupByProcessos(IEnumerable<ProcessoSync> processosSync)

        {
            return processosSync
                .AsParallel()
                .GroupBy(x => new
                {
                    x.Id,
                    x.Numero,
                    x.Titular,
                    x.CpfCnpjInpi,
                    x.TitularPais,
                    x.TitularPaisNome,
                    x.TitularUf,
                    x.TitularUfNome,
                    x.Procurador,
                    x.Marca,
                    x.MarcaOrtografada,
                    x.MarcaNaoOrtografada,
                    x.MarcaSemVogais,
                    x.Prioridade,
                    x.PrioridadeData,
                    x.PrioridadePais,
                    x.DataDeposito,
                    x.DataConcessao,
                    x.DataVigencia,
                    x.DataRegistro,
                    x.DataOrdinarioInicial,
                    x.DataOrdinarioFinal,
                    x.DataExtraOrdinarioInicial,
                    x.DataExtraOrdinarioFinal,
                    x.Apresentacao,
                    x.Natureza,
                    x.Especificacao,
                    x.Apostila
                }).Select(x => new ProcessoIndex()
                {
                    Id = x.Key.Id,
                    Numero = x.Key.Numero,
                    Titular = x.Key.Titular?.Trim(),
                    TitularPais = x.Key.TitularPais,
                    TitularPaisNome = x.Key.TitularPaisNome,
                    TitularUf = x.Key.TitularUf,
                    TitularUfNome = x.Key.TitularUfNome,
                    CpfCnpjInpi = x.Key.CpfCnpjInpi,
                    Procurador = x.Key.Procurador?.Trim(),
                    Marca = x.Key.Marca?.Trim(),
                    MarcaOrtografada = x.Key.MarcaOrtografada,
                    MarcaNaoOrtografada = x.Key.MarcaNaoOrtografada,
                    MarcaOrtografadaInverted = x.Key.MarcaOrtografada.GetStringReverse(),
                    MarcaNaoOrtografadaInverted = x.Key.MarcaNaoOrtografada.GetStringReverse(),
                    MarcaNaoOrtografadaNoSpace = x.Key.Marca.NotSpellOutNoSpaceManyWords(),
                    MarcaNaoOrtografadaNoSpaceInverted = x.Key.Marca.NotSpellOutNoSpaceManyWords().GetStringReverse(),
                    MarcaSemVogais = x.Key.MarcaSemVogais,
                    Prioridade = x.Key.Prioridade,
                    PrioridadeData = x.Key.PrioridadeData,
                    PrioridadePais = x.Key.PrioridadePais,
                    DataDeposito = x.Key.DataDeposito,
                    DataConcessao = x.Key.DataConcessao,
                    DataVigencia = x.Key.DataVigencia,
                    DataRegistro = x.Key.DataRegistro,
                    DataOrdinarioInicial = x.Key.DataOrdinarioInicial,
                    DataOrdinarioFinal = x.Key.DataOrdinarioFinal,
                    DataExtraOrdinarioInicial = x.Key.DataExtraOrdinarioInicial,
                    DataExtraOrdinarioFinal = x.Key.DataExtraOrdinarioFinal,
                    Apresentacao = x.Key.Apresentacao,
                    Natureza = x.Key.Natureza,
                    Especificacao = x.Key.Especificacao,
                    Apostila = x.Key.Apostila
                }).ToList();
        }

        private IEnumerable<Cfe4Sync> GroupByCfe4S(IEnumerable<ProcessoSync> processosSync)
        {
            return processosSync
                .AsParallel()
                .Where(x => !string.IsNullOrWhiteSpace(x.Cfe4))
                .GroupBy(x => new
                {
                    x.Numero,
                    x.Cfe4,
                    x.Cfe4Descricao
                }).Select(x => new Cfe4Sync()
                {
                    ProcessoNumero = x.Key.Numero,
                    Codigo = x.Key.Cfe4.Trim(),
                    Descricao = x.Key.Cfe4Descricao
                }).ToList();
        }

        private IEnumerable<DespachoSync> GroupByDespachos(IEnumerable<ProcessoSync> processosSync)
        {
            return processosSync
                .AsParallel()
                .Where(x => !string.IsNullOrWhiteSpace(x.DespachoCodigo))
                .GroupBy(x => new
                {
                    x.Numero,
                    x.DespachoRpi,
                    x.DespachoData,
                    x.DespachoCodigo,
                    x.DespachoDescricao,
                    x.DespachoDescricaoCompleta,
                    x.DespachoComplemento,
                    x.DespachoSituacao,
                    x.DespachoTipo,
                    x.DespachoTipoDescricao,
                    x.ProtocoloCodigo,
                    x.ProtocoloNumero,
                    x.ProtocoloData,
                    x.ProtocoloNomeRazaoSocial,
                    x.ProtocoloPais,
                    x.ProtocoloUf
                }).Select(x => new DespachoSync()
                {
                    ProcessoNumero = x.Key.Numero,
                    Rpi = x.Key.DespachoRpi,
                    Data = x.Key.DespachoData,
                    Codigo = x.Key.DespachoCodigo,
                    Descricao = x.Key.DespachoDescricao,
                    DescricaoCompleta = x.Key.DespachoDescricaoCompleta,
                    Complemento = x.Key.DespachoComplemento,
                    DespachoSituacao = x.Key.DespachoSituacao,
                    DespachoTipo = x.Key.DespachoTipo,
                    DespachoTipoDescricao = x.Key.DespachoTipoDescricao,
                    ProtocoloCodigo = x.Key.ProtocoloCodigo,
                    ProtocoloNumero = x.Key.ProtocoloNumero,
                    ProtocoloData = x.Key.ProtocoloData,
                    ProtocoloNomeRazaoSocial = x.Key.ProtocoloNomeRazaoSocial,
                    ProtocoloPais = x.Key.ProtocoloPais,
                    ProtocoloUf = x.Key.ProtocoloUf,
                    UltimoDespacho = false
                }).ToList();
        }

        private IEnumerable<ClasseSync> GroupByClasseInternacional(IEnumerable<ProcessoSync> processosSync)
        {
            return processosSync
                .Where(x => !string.IsNullOrWhiteSpace(x.ClasseInternacional))
                .AsParallel()
                .GroupBy(x => new
                {
                    x.Numero,
                    x.ClasseInternacional,
                    x.ClasseInternacionalEdicao,
                    x.ClasseInternacionalDescricao
                }).Select(x => new ClasseSync()
                {
                    ProcessoNumero = x.Key.Numero,
                    Codigo = x.Key.ClasseInternacional.Trim(),
                    Edicao = x.Key.ClasseInternacionalEdicao.TrimStart('0'),
                    Descricao = x.Key.ClasseInternacionalDescricao
                }).ToList();
        }

        private IEnumerable<ClasseSync> GroupByClasseNacional(IEnumerable<ProcessoSync> processosSync)
        {
            var classeGroupList = processosSync
                            .Where(x => !string.IsNullOrWhiteSpace(x.Classe1))
                            .AsParallel()
                            .GroupBy(x => new
                            {
                                x.Numero,
                                x.Classe1,
                                x.Classe1Sub,
                                x.Classe1SubDescricao,
                                x.Classe2,
                                x.Classe2Sub,
                                x.Classe2SubDescricao,
                                x.Classe3,
                                x.Classe3Sub,
                                x.Classe3SubDescricao
                            }).Select(x => new
                            {
                                ProcessoNumero = x.Key.Numero,
                                x.Key.Classe1,
                                SubClasse1 = x.Key.Classe1Sub,
                                SubClasse1Descricao = x.Key.Classe1SubDescricao,
                                x.Key.Classe2,
                                SubClasse2 = x.Key.Classe2Sub,
                                SubClasse2Descricao = x.Key.Classe2SubDescricao,
                                x.Key.Classe3,
                                SubClasse3 = x.Key.Classe3Sub,
                                SubClasse3Descricao = x.Key.Classe3SubDescricao
                            }).ToList();

            var auxClasseList = new List<ClasseSync>(classeGroupList.Count);

            classeGroupList
                .ForEach(x =>
                {
                    var classe1 = new ClasseSync()
                    {
                        ProcessoNumero = x.ProcessoNumero,
                        Codigo = x.Classe1?.Trim(),
                        SubClasse = new List<SubClasseSync>() { new SubClasseSync()
                        {
                            Codigo = x.SubClasse1?.Trim(),
                            Descricao = x.SubClasse1Descricao
                        } }
                    };

                    ClasseSync classe2 = null;
                    ClasseSync classe3 = null;

                    if (!string.IsNullOrWhiteSpace(x.Classe2))
                    {
                        if (x.Classe2.Trim() == x.Classe1.Trim())
                        {
                            if (x.SubClasse2 != x.SubClasse1)
                                classe1.SubClasse.Add(new SubClasseSync()
                                {
                                    Codigo = x.SubClasse2?.Trim(),
                                    Descricao = x.SubClasse2Descricao
                                });
                        }
                        else
                        {
                            classe2 = new ClasseSync()
                            {
                                ProcessoNumero = x.ProcessoNumero,
                                Codigo = x.Classe2?.Trim(),
                                SubClasse = new List<SubClasseSync>()
                                {
                                    new SubClasseSync()
                                    {
                                        Codigo = x.SubClasse2?.Trim(),
                                        Descricao = x.SubClasse2Descricao
                                    }
                                }
                            };
                        }

                        if (!string.IsNullOrWhiteSpace(x.Classe3))
                        {
                            if (x.Classe3.Trim() == x.Classe1.Trim())
                            {
                                if (x.SubClasse3 != x.SubClasse2 && x.SubClasse3 != x.SubClasse1)
                                {
                                    classe1.SubClasse.Add(new SubClasseSync()
                                    {
                                        Codigo = x.SubClasse3?.Trim(),
                                        Descricao = x.SubClasse3Descricao
                                    });
                                }
                            }
                            else if (x.Classe3.Trim() == x.Classe2.Trim())
                            {
                                if (x.SubClasse3 != x.SubClasse2)
                                    classe2.SubClasse.Add(new SubClasseSync()
                                    {
                                        Codigo = x.SubClasse3?.Trim(),
                                        Descricao = x.SubClasse3Descricao
                                    });
                            }
                            else
                            {
                                classe3 = new ClasseSync()
                                {
                                    ProcessoNumero = x.ProcessoNumero,
                                    Codigo = x.Classe3?.Trim(),
                                    SubClasse = new List<SubClasseSync>()
                                {
                                    new SubClasseSync()
                                    {
                                        Codigo = x.SubClasse3?.Trim(),
                                        Descricao = x.SubClasse3Descricao
                                    }
                                }
                                };
                            }
                        }
                    }

                    auxClasseList.Add(classe1);
                    if (classe2 != null)
                    {
                        auxClasseList.Add(classe2);
                        if (classe3 != null)
                        {
                            auxClasseList.Add(classe3);
                        }
                    }
                });

            return auxClasseList;
        }

        private IEnumerable<ClasseSync> GroupByClasse(IEnumerable<ProcessoSync> processosSync)
        {
            return processosSync
                .Where(x => !string.IsNullOrWhiteSpace(x.Classe))
                .AsParallel()
                .GroupBy(x => new
                {
                    x.Numero,
                    x.Classe,
                    x.ClasseEdicao,
                    x.ClasseDescricao,
                    x.ClasseStatus,
                    x.EspecificacaoNova
                }).Select(x => new ClasseSync()
                {
                    ProcessoNumero = x.Key.Numero,
                    Codigo = x.Key.Classe.Trim(),
                    Edicao = x.Key.ClasseEdicao.TrimStart('0'),
                    Descricao = x.Key.ClasseDescricao,
                    Status = x.Key.ClasseStatus,
                    Especificado = x.Key.EspecificacaoNova
                }).ToList();
        }

        /// <summary>
        /// The build processo with childs.
        /// </summary>
        /// <param name="processoList">The processo list. </param>
        /// <param name="cfe4List">The cfe 4 list.</param>
        /// <param name="despachoList">The despacho list.</param>
        /// <param name="classeList">The classe list.</param>
        private void BuildProcessoWithChilds(IEnumerable<ProcessoIndex> processoList, 
            IEnumerable<Cfe4Sync> cfe4List, 
            IEnumerable<DespachoSync> despachoList, 
            IEnumerable<ClasseSync> classeList)
        {
            processoList
                .AsParallel()
                .ForAll(x =>
                {
                    #region CFE4

                    //////x.Cfe4 = cfe4List.Where(c => c.ProcessoNumero == x.Numero).ToList();

                    ////var auxList = cfe4List.Where(c => c.ProcessoNumero == x.Numero).ToList();
                    ////auxList.AsParallel().ForAll(a => a.ProcessoNumero = null);

                    ////if (auxList.Count > 0)
                    ////    x.Cfe4 = auxList;

                    if (cfe4List.Any(c => c.ProcessoNumero == x.Numero))
                    {
                        x.Cfe4 = cfe4List.Where(c => c.ProcessoNumero == x.Numero)
                            .Select(c => new Cfe4Sync
                            {
                                Codigo = c.Codigo,
                                Descricao = c.Descricao
                            })
                            .ToList();
                    }

                    #endregion CFE4

                    #region Despacho

                    //x.Despacho = despachoList.Where(d => d.ProcessoNumero == x.Numero)
                    //                                  .OrderBy(d => d.Rpi)
                    //                                  .ThenBy(d => d.Codigo)
                    //                                  .ToList();

                    var auxDespachoList = despachoList.Where(d => d.ProcessoNumero == x.Numero)
                                                      .OrderBy(d => d.Rpi)
                                                      .ThenBy(d => d.Codigo)
                                                      .ToList();

                    var ultimoDespacho = auxDespachoList.OrderByDescending(y => y.Rpi)
                                                        .ThenByDescending(y => y.Codigo)
                                                        .FirstOrDefault();
                    if (ultimoDespacho != null)
                        ultimoDespacho.UltimoDespacho = true;
                    auxDespachoList.AsParallel().ForAll(a => a.ProcessoNumero = null);

                    if (auxDespachoList.Count > 0)
                        x.Despacho = auxDespachoList;

                    #endregion Despacho

                    #region Classe

                    //x.Classe = classeList.Where(ci => ci.ProcessoNumero == x.Numero).ToList();

                    var auxClasseList =
                        classeList.Where(ci => ci.ProcessoNumero == x.Numero).ToList();
                    auxClasseList.AsParallel().ForAll(a => a.ProcessoNumero = null);

                    if (auxClasseList.Count > 0)
                        x.Classe = auxClasseList;

                    #endregion Classe

                });

        }

        private void CreateIndex()
        {
            var indexSettings = new IndexSettings { NumberOfReplicas = 1, NumberOfShards = 1 };

            if (client.IndexExists("hcoffice").Exists == false)
                client.CreateIndex("hcoffice");
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="indexList">
        /// The index list.
        /// </param>
        /// <param name="nameIndex">
        /// The name index.
        /// </param>
        /// <typeparam name="T">
        /// Generic Type of index
        /// </typeparam>
        /// <returns>
        /// Returns if the sync worked
        /// </returns>
        private bool Index<T>(IEnumerable<T> indexList, string nameIndex) where T : class
        {
            // TO DO avaliar se uso o _client que é inicializado ao criar a estância da classe de sincronização com o elastic
            var client = ElasticSearchHelper.GetElasticClient(nameIndex);
            return client.Bulk(b => b.IndexMany(indexList)).IsValid;
        }

        private bool IndexClasseAfinifades(IEnumerable<ClasseAfinidadeIndex> classeAfinidadeList)
        {
            var client = ElasticSearchHelper.GetElasticClient("ClasseAfinidade");
            return client.Bulk(b => b.IndexMany(classeAfinidadeList)).IsValid;
        }

        private void RestartService(string serviceName, int timeoutMilliseconds)
        {
            var service = new ServiceController(serviceName);
            try
            {
                var millisec1 = Environment.TickCount;
                var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                var millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                throw new Exception("Não foi posível fazer o restart do serviço!");
            }
        }

        #endregion Private Methods
    }
}