namespace BusinessLayer.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DTOLayer;
    using DTOLayer.Enum;
    using DTOLayer.Indexes;

    using Nest;

    /// <summary>
    /// The process search.
    /// </summary>
    public class ProcessSearch
    {
        /// <summary>
        /// The _elastic client.
        /// </summary>
        private readonly IElasticClient _elasticClient;

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessSearch"/> class.
        /// </summary>
        /// <param name="elasticClient">
        /// The elastic client.
        /// </param>
        public ProcessSearch(IElasticClient elasticClient)
        {
            this._elasticClient = elasticClient;
        }

        /// <summary>
        /// The get count all processes in index of processes.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long GetCountAllProcesses()
        {
            return _elasticClient.GetCountAllProcesses<ProcessoIndex>();
        }

        /// <summary>
        /// The filter full processo by numero.
        /// </summary>
        /// <param name="numero">
        /// The numero.
        /// </param>
        /// <returns>
        /// The <see cref="DetailResultSearch"/>.
        /// </returns>
        public DetailResultSearch FilterFullProcessoByNumero(string numero)
        {
            var filter = this._elasticClient.FilterBy<ProcessoIndex>(p => p.Numero, numero);
            return BuildDetailResultSearch(filter);
        }

        /// <summary>
        /// Apply filters of search
        /// </summary>
        /// <param name="filterList">The filter list</param>
        /// <param name="pagination">The pagination</param>
        /// <returns>Returns the result of search</returns>
        public ResultSearch Filter(IEnumerable<FilterSearchModel> filterList, Pagination pagination)
        {
            var filter = _elasticClient.FilterBy<ProcessoIndex>(BuildRequestWithShouldTerm(filterList, pagination));
            return BuildResultSearch(filter);
        }

        /// <summary>
        /// The filter classe afinidade.
        /// </summary>
        /// <param name="filterList">
        /// The filter list.
        /// </param>
        /// <param name="pagination">
        /// The pagination.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ClasseAfinidadeIndex> FilterClasseAfinidade(IEnumerable<FilterSearchModel> filterList, Pagination pagination)
        {
            return _elasticClient.FilterBy<ClasseAfinidadeIndex>(BuildRequestWithShouldTerm(filterList, pagination))
                .Documents
                .Select(x => x)
                .ToList();
        }

        public long GetLastRpiImported()
        {
            this._elasticClient.Search<ProcessoIndex>(
                x => x.Aggregations(
                    a => a.Nested(
                        "despacho",
                        n => n.Path(p => p.Despacho)
                            .Aggregations(ad => ad.Max("max_rpi", m => m.Field(p => p.Despacho))))));
            //_elasticClient.Search<ProcessoIndex>(
            //    x => x.Aggregations(a => a
            //        .Nested("despacho", n => n)
            //        .Max("max_rpi", m => m.Field(f => f.Despacho))));

            return Int64.MaxValue;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Build the values used in the Results view
        /// </summary>
        /// <param name="filter">Results of search</param>
        /// <returns>Returns the response of request</returns>
        private ResultSearch BuildResultSearch(ISearchResponse<ProcessoIndex> filter)
        {
            return new ResultSearch()
            {
                TotalOfSearch = filter.Total,
                ProcessoList = filter.Documents.Any()
                                ? filter.Documents.Select(x => x).ToList()
                                : null,
                TotalProcesses = this.GetCountAllProcesses()
            };
        }

        /// <summary>
        /// Build the values used in the Detail Result view
        /// </summary>
        /// <param name="resultByFilter">Results of search by Filter</param>
        /// <returns>Returns the Details of result</returns>
        private DetailResultSearch BuildDetailResultSearch(ISearchResponse<ProcessoIndex> resultByFilter)
        {
            var numeroProcesso = resultByFilter.Documents.FirstOrDefault()?.Numero;

            if (string.IsNullOrWhiteSpace(numeroProcesso))
                return null;

            var result = FilterProcessoInComplemento(numeroProcesso);

            var citacoes = new List<Citacao>(result.Select(x => new Citacao()
            {
                Numero = x.Numero,
                DataDeposito = x.DataDeposito,
                Marca = x.Marca,
                Classe = x.Classe,
                Despacho = x.Despacho.FindAll(p => p.Complemento != null && p.Complemento.Contains(numeroProcesso)),
                Apresentacao = x.Apresentacao,
                Titular = x.Titular
            }));

            return new DetailResultSearch()
            {
                Processo = resultByFilter.Documents.FirstOrDefault(),
                CitacaoList = citacoes.Any()
                                        ? citacoes
                                        : null
            };
        }

        /// <summary>
        /// Do to search a process in complement of despacho
        /// </summary>
        /// <param name="numero"></param>
        /// <returns>Returns the </returns>
        private IEnumerable<ProcessoIndex> FilterProcessoInComplemento(string numero)
        {
            return this._elasticClient.FilterBy<ProcessoIndex>("despacho.complemento", numero);
        }

        /// <summary>
        /// Build a request with any terms to search with logic OR in combination
        /// </summary>
        /// <param name="filterList">Filters to apply</param>
        /// <param name="pagination">Pagination of results</param>
        /// <returns>Returns the result of search request</returns>
        private SearchRequest BuildRequestWithShouldTerm(IEnumerable<FilterSearchModel> filterList, Pagination pagination)
        {
            var queryContainer = new QueryContainer();
            filterList
                .ToList()
                .ForEach(f =>
                {
                    switch (f.Combination)
                    {
                        case Combination.And:
                            queryContainer &= BuildQueryContainer(f);
                            break;
                        case Combination.Or:
                            queryContainer |= BuildQueryContainer(f);
                            break;
                        case Combination.Except:
                            queryContainer &= !BuildQueryContainer(f);
                            break;
                    }
                });

            var searchRequest = new SearchRequest
            {
                From = pagination.PageNumber,
                Size = pagination.PageSize,
                Query = queryContainer
            };

            var valueWithSorted = filterList.FirstOrDefault(x => x.SortList != null && x.SortList.Any());
            if (valueWithSorted?.SortList != null)
                searchRequest.Sort = valueWithSorted.SortList;


            return searchRequest;
        }

        /// <summary>
        /// The build query container with all filters
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="QueryContainer"/>.
        /// </returns>
        private QueryContainer BuildQueryContainer(FilterSearchModel filter)
        {
            var result = new QueryContainer();
            filter.Values.ForEach(v => result |= BuildQueryContainerOfSameField(filter, v));

            return result;
        }

        /// <summary>
        /// The build query container of same field.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="QueryContainer"/>.
        /// </returns>
        private QueryContainer BuildQueryContainerOfSameField(FilterSearchModel filter, Tuple<string, string> value)
        {
            var result = new QueryContainer();

            switch (filter.Type)
            {
                case TypeSearch.Match:
                    result = new MatchQuery
                    {
                        Field = filter.Field,
                        Query = value.Item1
                    };
                    break;
                case TypeSearch.Term:
                    result = new TermQuery
                    {
                        Field = filter.Field,
                        Value = value.Item1
                    };
                    break;
                case TypeSearch.MatchPhrase:
                    result = new MatchPhraseQuery
                    {
                        Field = filter.Field,
                        Query = value.Item1,
                        Slop = 95
                    };
                    break;
                case TypeSearch.WildCard:
                    result = new WildcardQuery
                    {
                        Field = filter.Field,
                        Value = value.Item1
                    };
                    break;
                case TypeSearch.Prefix:
                    result = new PrefixQuery
                    {
                        Field = filter.Field,
                        Value = value.Item1
                    };
                    break;
                case TypeSearch.DateRange:
                    result = new DateRangeQuery
                    {
                        Field = filter.Field,
                        GreaterThanOrEqualTo = value.Item1,
                        LessThanOrEqualTo = value.Item2
                    };
                    break;
            }

            return result;
        }

        #endregion Private Methods
    }
}
