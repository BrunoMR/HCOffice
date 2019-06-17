namespace SistemaWeb.Controllers
{
    using ViewModels;
    using System.Web.Mvc;
    using DTOLayer;
    using BusinessLayer.Search;
    using BusinessLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DTOLayer.Enum;
    using DTOLayer.Indexes;
    using Nest;
    using Newtonsoft.Json;
    using PagedList;
    using Utils;

    [RoutePrefix("Busca")]
    public class SearchController : Controller
    {
        private static List<FilterSearchModel> _filters = new List<FilterSearchModel>();

        // GET: Search

        #region Public Methods

        [Route("Entrada")]
        public ActionResult Index()
        {
            CreateDropDownLists();
            return View("Filter");
        }

        [Route("Limpar")]
        public ActionResult Cleaner()
        {
            ModelState.Clear();
            CreateDropDownLists();

            return View("Filter");
        }

        [HttpGet]
        [Route("Filtrar")]
        public ActionResult ApplyFilter(FilterViewModel filter)
        {
            _filters = new List<FilterSearchModel>();

            if (filter.Numero?.Count > 0)
            {
                return View("DetailResults", BuildDetailResultsModel(filter.Numero));
            }

            if (string.IsNullOrWhiteSpace(filter.Marca) == false)
            {
                var marcaOrtografafadaOrNot = OrtografafadaOrNot(filter);

                if (filter.Radical || filter.Prefix || filter.Suffix || filter.ExactBrand || filter.ExactWord)
                {
                    if (filter.ExactWord)
                        _filters.Add(new FilterSearchModel("marca.case_insensitive", RetrieveTupleListWithEmptySecondValue(filter.Marca.ToUpper()), TypeSearch.Match));

                    if (filter.ExactBrand)
                        _filters.Add(new FilterSearchModel("marca.not_analyzed", RetrieveTupleListWithEmptySecondValue(filter.Marca.ToUpper()), TypeSearch.Term));

                    var brandSpellOrNotSpell = DoSpellOrNotSpell(filter, marcaOrtografafadaOrNot);

                    if (filter.Radical)
                    {
                        _filters.Add(new FilterSearchModel("marca.case_insensitive", RetrieveTupleListWithEmptySecondValue("*" + filter.Marca.ToUpper().RemoveDiacritics() + "*"), TypeSearch.WildCard));

                        var field = marcaOrtografafadaOrNot + ".case_insensitive";
                        
                        _filters.Add(new FilterSearchModel(field, RetrieveTupleListWithEmptySecondValue(RetrieveWordWithWildcard(brandSpellOrNotSpell)), TypeSearch.WildCard, Combination.Or));
                    }

                    if (filter.Prefix)
                    {
                        var field = marcaOrtografafadaOrNot + ".case_insensitive";

                        _filters.Add(new FilterSearchModel(field, RetrieveTupleListWithEmptySecondValue(brandSpellOrNotSpell.ToUpper()), TypeSearch.Prefix));
                    }

                    if (filter.Suffix)
                    {
                        var field = marcaOrtografafadaOrNot + "Inverted";

                        _filters.Add(new FilterSearchModel(field, RetrieveTupleListWithEmptySecondValue(brandSpellOrNotSpell.GetStringReverse().ToLower()), TypeSearch.Prefix));
                    }
                }
                else
                {
                    _filters.Add(new FilterSearchModel("marca.case_insensitive", RetrieveTupleListWithEmptySecondValue("*" + filter.Marca.ToUpper().RemoveDiacritics() + "*"), TypeSearch.WildCard));

                    var field = marcaOrtografafadaOrNot + ".case_insensitive";

                    if (filter.Phonetic)
                    {
                        _filters.Add(new FilterSearchModel(field, RetrieveTupleListWithEmptySecondValue(filter.Marca
                            .Spell()
                            .Phonetic()
                            .Select(x => "*" + x + "*")), TypeSearch.WildCard, Combination.Or));

                        _filters.Add(new FilterSearchModel("marcaSemVogais", RetrieveTupleListWithEmptySecondValue("*" + filter.Marca.RetirarVogais().ToLower() + "*"), TypeSearch.WildCard, Combination.Or));
                    }
                    else
                    {
                        _filters.Add(new FilterSearchModel(field, RetrieveTupleListWithEmptySecondValue("*" + NotSpellWithSpaceOrNot(filter) + "*"), TypeSearch.WildCard, Combination.Or));
                    }
                }

            }

            if (filter.Classe?.Count > 0)
            {
                _filters.Add(filter.Affinity
                    ? new FilterSearchModel("classe.codigo",
                        RetrieveTupleListWithEmptySecondValue(BuildFilterOfClasseAndClasseAfinidades(filter)),
                        TypeSearch.Term)
                    : new FilterSearchModel("classe.codigo", RetrieveTupleListWithEmptySecondValue(filter.Classe),
                        TypeSearch.Term));
            }

            if (string.IsNullOrWhiteSpace(filter.Especificacao) == false)
            {
                _filters.Add(new FilterSearchModel("especificacao.case_insensitive", RetrieveTupleListWithEmptySecondValue("*" + filter.Especificacao.ToUpper().RemoveDiacritics() + "*"), TypeSearch.Match));
            }

            if (string.IsNullOrWhiteSpace(filter.DespachoComplemento) == false)
            {
                _filters.Add(new FilterSearchModel("despacho.complemento.case_insensitive", RetrieveTupleListWithEmptySecondValue("*" + filter.DespachoComplemento.ToUpper().RemoveDiacritics() + "*"), TypeSearch.Match));
            }

            if (filter.Cfe4?.Count > 0)
            {
                _filters.Add(new FilterSearchModel("cfe4.codigo", RetrieveTupleListWithEmptySecondValue(filter.Cfe4), TypeSearch.Term));
            }

            if (string.IsNullOrWhiteSpace(filter.CpfCnpjInpi) == false)
            {
                _filters.Add(new FilterSearchModel("cpfCnpjInpi", RetrieveTupleListWithEmptySecondValue(filter.CpfCnpjInpi), TypeSearch.Match));
            }

            if (filter.Titular?.Count > 0)
            {
                _filters.Add(new FilterSearchModel("titular.not_analyzed", RetrieveTupleListWithEmptySecondRawValueWithSpaceEnds(filter.Titular), TypeSearch.Term, Combination.Or));
				//_filters.Add(new FilterSearchModel("titular.case_insensitive", RetrieveTupleListWithEmptySecondValue(filter.Titular), TypeSearch.Match));
			}

            if (filter.Procurador?.Count > 0)
            {
                _filters.Add(new FilterSearchModel("procurador.not_analyzed", RetrieveTupleListWithEmptySecondRawValueWithSpaceEnds(filter.Procurador), TypeSearch.Term, Combination.Or));
                //_filters.Add(new FilterSearchModel("procurador.case_insensitive", RetrieveTupleListWithEmptySecondValue(filter.Procurador), TypeSearch.Match));
            }

            if (string.IsNullOrWhiteSpace(filter.Apostila) == false)
            {
                _filters.Add(new FilterSearchModel("apostila.case_insensitive", RetrieveTupleListWithEmptySecondValue("*" + filter.Apostila.ToUpper().RemoveDiacritics() + "*"), TypeSearch.Match));
            }

            if (filter.Rpi > 0)
            {
                _filters.Add(new FilterSearchModel("despacho.rpi", RetrieveTupleListWithEmptySecondValue(filter.Rpi.ToString()), TypeSearch.Match));
            }

            if (filter.Apresentacao?.Count > 0)
            {
                _filters.Add(new FilterSearchModel("apresentacao", RetrieveTupleListWithEmptySecondValue(filter.Apresentacao), TypeSearch.Match));
            }

            if (filter.Pais?.Count > 0)
            {
                _filters.Add(new FilterSearchModel("titularPaisNome", RetrieveTupleListWithEmptySecondValue(filter.Pais), TypeSearch.MatchPhrase));
            }

            if (filter.Estado?.Count > 0)
            {
                _filters.Add(new FilterSearchModel("titularUfNome", RetrieveTupleListWithEmptySecondValue(filter.Estado), TypeSearch.MatchPhrase));
            }

            if (filter.Despacho?.Count > 0)
            {
                if (filter.LastDespacho)
                {
                    filter.Despacho
                        .ForEach(x =>
                        {
                            _filters.Add(new FilterSearchModel("despacho.codigo", RetrieveTupleListWithEmptySecondValue(x), TypeSearch.Match));
                            _filters.Add(new FilterSearchModel("despacho.ultimoDespacho", RetrieveTupleListWithEmptySecondValue("true"), TypeSearch.MatchPhrase));
                        });
                }
                else
                    _filters.Add(new FilterSearchModel("despacho.codigo", RetrieveTupleListWithEmptySecondValue(filter.Despacho), TypeSearch.Match));
            }

            if (filter.ShowImages)
            {
                _filters.Add(new FilterSearchModel("apresentacao", RetrieveTupleListWithEmptySecondValue("Nominativa"), TypeSearch.Match, Combination.Except));
            }

            if (!filter.Extinct && !filter.OnlyExtinct)
            {
                //AddLastDespachoToFilter();

                var valueList = new List<string> { "Arquivado", "Extinto" };
                _filters.Add(new FilterSearchModel("despacho.despachoSituacao", RetrieveTupleListWithEmptySecondValue(valueList), TypeSearch.MatchPhrase, Combination.Except));
            }

            if (filter.OnlyExtinct)
            {
                //AddLastDespachoToFilter();

                var valueList = new List<string> { "Arquivado", "Extinto" };
                _filters.Add(new FilterSearchModel("despacho.despachoSituacao", RetrieveTupleListWithEmptySecondValue(valueList), TypeSearch.MatchPhrase));
            }

            if (filter.DataConcessaoStart > DateTime.MinValue)
            {
                _filters.Add(new FilterSearchModel("dataConcessao", RetrieveTupleList(filter.DataConcessaoStart.ToString(), filter.DataConcessaoEnd.ToString()), TypeSearch.DateRange));
            }

            if (filter.DataDepositoStart > DateTime.MinValue)
            {
                _filters.Add(new FilterSearchModel("dataDeposito", RetrieveTupleList(filter.DataDepositoStart.ToString(), filter.DataDepositoEnd.ToString()), TypeSearch.DateRange));
            }

            if (_filters.Any())
            {
                var filterToSort = _filters.FirstOrDefault();

                filterToSort.SortList = new List<ISort>
                {
                    new SortField
                    {
                        Field = "marca.not_analyzed",
                        Order = SortOrder.Ascending
                    }//,
                    //new SortField
                    //{
                    //    Field = "titular",
                    //    Order = SortOrder.Ascending
                    //}
                };
            }

            return FilterInElasticSearch();
        }

        private string DoSpellOrNotSpell(FilterViewModel filter, string marcaOrtografafadaOrNot)
        {
            var value = marcaOrtografafadaOrNot.Equals("marcaOrtografada")
                ? filter.Marca.Spell()
                : NotSpellWithSpaceOrNot(filter);
            return value;
        }

        public ActionResult FilterInElasticSearch(int page = 1)
        {
            var pagination = new Pagination(page, 100);

            var processoSearch = new ProcessSearch(ElasticSearchHelper.GetElasticClient("Main"));
            var resultSearch = processoSearch.Filter(_filters, pagination);
            var resultSearchViewModel = Mapper.Map<ResultSearchViewModel>(resultSearch);

            var listResultSearchPaginated = new StaticPagedList<ResultSearchViewModel>(new List<ResultSearchViewModel> { resultSearchViewModel }, page, pagination.PageSize, resultSearchViewModel.TotalOfSearch);

            return View("ResultSearch", listResultSearchPaginated);
        }

        [HttpGet]
        [Route("Imagens")]
        public ActionResult JustImagens(int page = 1)
        {
            _filters.Add(new FilterSearchModel
            {
                Field = "apresentacao",
                Type = TypeSearch.Match,
                Values = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("Nominativa", string.Empty)
                },
                Combination = Combination.Except
            });

            return FilterJustImagesInElasticSearch();
        }

        public ActionResult FilterJustImagesInElasticSearch(int page = 1)
        {
            var pagination = new Pagination(page, 100);

            var processoSearch = new ProcessSearch(ElasticSearchHelper.GetElasticClient("Main"));
            var resultSearch = processoSearch.Filter(_filters, pagination);
            var resultSearchViewModel = Mapper.Map<ResultSearchViewModel>(resultSearch);

            var listResultSearchPaginated = new StaticPagedList<ResultSearchViewModel>(new List<ResultSearchViewModel> { resultSearchViewModel }, page, pagination.PageSize, resultSearchViewModel.TotalOfSearch);

            return View("JustImagensSearch", listResultSearchPaginated);
        }

        [Route("Detalhe/{code}")]
        public ActionResult DetailResult(string code)
        {
            var processoSearch = new ProcessSearch(ElasticSearchHelper.GetElasticClient("Main"));
            var detailResultSearch = processoSearch.FilterFullProcessoByNumero(code);

            var detailResultSearchViewModel = Mapper.Map<DetailSearchViewModel>(detailResultSearch);
            return PartialView("DetailResult", detailResultSearchViewModel);
        }

        [Route("Detalhes")]
        public ActionResult DetailResults(List<string> codes)
        {
            return View("DetailResults", BuildDetailResultsModel(codes));
        }

        [Route("DetalheProcess/{code}")]
        public ActionResult DetailProcess(string code)
        {
            return View("DetailResults", BuildDetailResultsModel(code));
        }

        public ActionResult SearchTitular(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<TitularIndex>("Titular", request, @from, size);
        }

        public ActionResult SearchProcurador(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<ProcuradorIndex>("Procurador", request, @from, size);
        }

        public ActionResult SearchClasse(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<ClasseIndex>("Classe", request, @from, size);
        }

        public ActionResult SearchCfe4(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<Cfe4Index>("Cfe4", request, @from, size);
        }

        public ActionResult SearchDespacho(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<DespachoIndex>("Despacho", request, @from, size);
        }

        public ActionResult SearchApresentacao(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<ApresentacaoIndex>("Apresentacao", request, @from, size);
        }

        public ActionResult SearchPais(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<PaisIndex>("Pais", request, @from, size);
        }

        public ActionResult SearchEstado(string request, int from, int size)
        {
            return RetrieveSearchToSelect2<EstadoIndex>("Estado", request, @from, size);
        }

        #endregion Public Methods

        #region Private Methods

        private void CreateDropDownLists()
        {
            ViewBag.Numero = new SelectList(new List<string> { string.Empty });
            ViewBag.Procurador = new SelectList(new List<string> { string.Empty });
            ViewBag.Titular = new SelectList(new List<string> { string.Empty });
            ViewBag.Despacho = new SelectList(new List<string> { string.Empty });
            ViewBag.Classe = new SelectList(new List<string> { string.Empty });
            ViewBag.Cfe4 = new SelectList(new List<string> { string.Empty });
            ViewBag.Apresentacao = new SelectList(new List<string> { string.Empty });
            ViewBag.Pais = new SelectList(new List<string> { string.Empty });
            ViewBag.Estado = new SelectList(new List<string> { string.Empty });
        }

        private static List<string> BuildFilterOfClasseAndClasseAfinidades(FilterViewModel filter)
        {
            var filterClasseAfinidade = new List<FilterSearchModel>
            {
                    new FilterSearchModel
                    {
                        Field = "classeA",
                        Type = TypeSearch.Term,
                        Values = filter.Classe.Select(s => new Tuple<string, string>(s, string.Empty)).ToList()
                    }
                };

            var classeAfinidadeSearch = new ProcessSearch(ElasticSearchHelper.GetElasticClient("ClasseAfinidade"));
            var resultClasseAfinidadeSearch = classeAfinidadeSearch.FilterClasseAfinidade(filterClasseAfinidade, new Pagination(1, 100));

            var valuesOfClasses = new List<string>();
            valuesOfClasses.AddRange(filter.Classe);

            //valuesOfClasses =
            //    resultClasseAfinidadeSearch.FindAll(x => (x.ClasseAfinidade.Any(a => !a.Contains(x.ClasseA))))
            //    .Select(x => x.ClasseAfinidade).ToList();

            resultClasseAfinidadeSearch.ForEach(c =>
            {
                valuesOfClasses.AddRange(c.ClasseAfinidade.Where(a => !a.Contains(c.ClasseA)).ToList());
            });

            return valuesOfClasses;
        }

        private DetailResultsViewModel BuildDetailResultsModel(List<string> codes)
        {
            return new DetailResultsViewModel
            {
                ProcessoList = codes
            };
        }

        private DetailResultsViewModel BuildDetailResultsModel(string code)
        {
            return new DetailResultsViewModel
            {
                ProcessoList = new List<string> { code }
            };
        }

        private void AddLastDespachoToFilter()
        {
            _filters.Add(new FilterSearchModel("despacho.ultimoDespacho", RetrieveTupleListWithEmptySecondValue("true"), TypeSearch.MatchPhrase));
        }

        private string RetrieveWordWithWildcard(string word)
        {
            return "*" + word.ToUpper() + "*";
        }

        private string OrtografafadaOrNot(FilterViewModel filter)
        {
            var notSpell = filter.WithSpace
                ? "marcaNaoOrtografada"
                : "marcaNaoOrtografadaNoSpace";

            return filter.Phonetic 
                ? "marcaOrtografada" 
                : notSpell;
        }

        private string NotSpellWithSpaceOrNot(FilterViewModel filter)
        {
            return filter.WithSpace
                ? filter.Marca.NotSpellManyWords()
                : filter.Marca.NotSpellOutNoSpaceManyWords();
        }

        private ActionResult RetrieveSearchToSelect2<T>(string index, string request, int from, int size) where T : class
        {
            var client = ElasticSearchHelper.GetElasticClient(index);
            var resp = client.Search<T>(s => s
                .From(from)
                .Size(size)
                .Query(q =>
                    q.Raw(request))
            );

            return Json(new { resp.Hits, resp.Total }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Tuple<string, string>> RetrieveTupleListWithEmptySecondValue(IEnumerable<string> valueList)
        {
            return valueList.Select(x => new Tuple<string, string>(x.ToUpper().Trim(), string.Empty)).ToList();
        }

        private IEnumerable<Tuple<string, string>> RetrieveTupleListWithEmptySecondRawValue(IEnumerable<string> valueList)
        {
            return valueList.Select(x => new Tuple<string, string>(x.Trim(), string.Empty)).ToList();
        }

		/// <summary>
		/// Método deve ser excluído depois, foi feito para funcionar por enquanto a pesquisa por procurador e titular
		/// </summary>
		/// <param name="valueList"></param>
		/// <returns></returns>
		private IEnumerable<Tuple<string, string>> RetrieveTupleListWithEmptySecondRawValueWithSpaceEnds(IEnumerable<string> valueList)
		{
			var returnList = valueList.Select(x => new Tuple<string, string>(x.Trim(), string.Empty)).ToList();
			returnList.AddRange(valueList.Select(x => new Tuple<string, string>(x + " ", string.Empty)).ToList());

			return returnList;
		}

		private IEnumerable<Tuple<string, string>> RetrieveTupleListWithEmptySecondValue(string value)
        {
            return new List<Tuple<string, string>>
            {
                new Tuple<string, string>(value, string.Empty)
            };
        }

        private IEnumerable<Tuple<string, string>> RetrieveTupleList(string valueOne, string valueTwo)
        {
            return new List<Tuple<string, string>>
            {
                new Tuple<string, string>(valueOne, valueTwo)
            };
        }

        #endregion Private Methods

    }

    public class Resolver : ElasticContractResolver
    {
        public Resolver(IConnectionSettingsValues connectionSettings, IList<Func<Type, JsonConverter>> contractConverters)
            : base(connectionSettings, contractConverters)
        {
        }
    }
}