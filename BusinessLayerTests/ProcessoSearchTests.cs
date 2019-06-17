using System;
using System.Collections.Generic;
using System.Globalization;
using DTOLayer;
using DTOLayer.Enum;
using Utils;

namespace BusinessLayerTests
{
    using BusinessLayer;
    using BusinessLayer.Search;
    using Nest;
    using System.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;

    public class ProcessoSearchTests
    {
        private readonly ProcessSearch processSearch;

        public ProcessoSearchTests(ITestOutputHelper output)
        {
            this.processSearch = new ProcessSearch(ElasticSearchHelper.GetElasticClient("Main"));
        }

        //[Fact]
        //public void FilterByMarcaShouldBeOk()
        //{
        //    var tempo = Stopwatch.StartNew();
        //    var resultSearch = this.processSearch.FilterByMarca("LIGAÇÃO DIRETA");  //("CAMPOL");
        //    // LIGAÇÃO DIRETA
        //    tempo.Stop();
        //    Assert.NotEmpty(resultSearch.ProcessoList);
        //}

        //[Fact]
        //public void FilterByNumeroProcessoShouldBeOk()
        //{
        //    var resultSearch = this.processSearch.FilterByProcesso("814584969");

        //    Assert.NotEmpty(resultSearch.ProcessoList);
        //}

        //[Fact]
        //public void FilterByClasseShouldBeOk()
        //{
        //    var tempo = Stopwatch.StartNew();
        //    var result = this.processSearch.FilterByClasse("30", new Pagination(0, 100));
        //    tempo.Stop();

        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public void FilterByEspecificacaoClasseShouldBeOk()
        //{
        //    var resultSearch = this.processSearch.FilterByEspecificacaoClasse("BISCOITOS");

        //    Assert.NotEmpty(resultSearch.ProcessoList);
        //}

        //[Fact]
        //public void FilterByCfe4ShouldBeOk()
        //{
        //    var processes = this.processSearch.FilterByCfe("5.3.11");

        //    Assert.NotEmpty(processes);
        //}

        //[Fact]
        //public void FilterByCpfCnpjInpiShouldBeOk()
        //{
        //    var processes = this.processSearch.FilterByCfe("5.3.11");

        //    Assert.NotEmpty(processes);
        //}

        [Fact]
        public void ShouldRetriveCountOfAllProcesses()
        {
            var total = this.processSearch.GetCountAllProcesses();

            Assert.NotNull(total);
            Assert.True(total > 0);
        }

        [Fact]
        public void FilterAllProcessoByNumeroShouldBeOk()
        {
            var resultSearch = this.processSearch.FilterFullProcessoByNumero("811542424");

            Assert.NotEmpty(resultSearch.Processo.Numero);
        }

        [Fact]
        public void FilterManyProcessosShouldBeOk()
        {
            var resultSearch = this.processSearch.FilterFullProcessoByNumero("811542424");

            Assert.NotEmpty(resultSearch.Processo.Numero);
        }

        //[Fact]
        //public void FilterManyClassesShouldBeOk()
        //{
        //    var resultSearch = this.processSearch.FilterManyProcessoByNumero();

        //    Assert.NotEmpty(resultSearch.Processo.Numero);
        //}

        [Fact]
        public void FilterManyValueShouldBeOk()
        {
            var filters = new List<FilterSearchModel>()
            {
                new FilterSearchModel()
                {
                    Field = "marca",
                    Values = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("big frango", string.Empty)
                    },
                    Type = TypeSearch.Match
                },
                new FilterSearchModel()
                {
                    Field = "apresentacao",
                    Values = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Mista", string.Empty)
                    },
                    Type = TypeSearch.Match
                },
                new FilterSearchModel()
                {
                    Field = "classe.codigo",
                    Values = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("25", string.Empty),
                        new Tuple<string, string>("30", string.Empty)
                    },
                    Type = TypeSearch.Term
                }
            };

            var resultSearch = this.processSearch.Filter(filters, new Pagination(1, 50));

            Assert.NotEmpty(resultSearch.ProcessoList);
        }

        //[Fact]
        //public void FilterClasseAfinidadeShouldBeOk()
        //{
        //    var classeAfinidadeSearch = new ProcessSearch(ElasticSearchHelper.GetElasticClient("ClasseAfinidade"));

        //    var filters = new List<FilterSearchModel>()
        //    {
        //        new FilterSearchModel()
        //        {
        //            Field = "classeA",
        //            Values = new List<string>() {"25", "30"},
        //            Type = TypeSearch.Term
        //        }
        //    };

        //    var resultSearch = classeAfinidadeSearch.FilterClasseAfinidade(filters, new Pagination(1, 50));

        //    Assert.NotEmpty(resultSearch);
        //}

        [Fact]
        public void FilterDateRangeShouldBeOk()
        {
            var filters = new List<FilterSearchModel>()
            {
                new FilterSearchModel()
                {
                    Field = "dataDeposito",
                    Values = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>(DateTime.Now.AddYears(-6).Date.ToString("yyyy-MM-dd"), DateTime.Now.AddYears(-6).Date.ToString("yyyy-MM-dd"))
                    },
                    Type = TypeSearch.DateRange
                }
            };

            var resultSearch = this.processSearch.Filter(filters, new Pagination(1, 50));

            Assert.NotEmpty(resultSearch.ProcessoList);
        }

        [Fact]
        public void Filter_Radical_Should_Be_Ok()
        {
            var marcaOrtograda = "cacau dias".Spell();
            var nameOfNgram = "ngram_with_" + marcaOrtograda.GetAmountLettersOfRadical();
            var radicalListOfMarca = new List<Tuple<string, string>>();

            marcaOrtograda
                .GetRadical()
                .ForEach(x => radicalListOfMarca.Add(new Tuple<string, string>(x, string.Empty)));

            var filters = new List<FilterSearchModel>()
            {
                new FilterSearchModel()
                {
                    Field = "marcaOrtografada." + nameOfNgram,
                    Values = radicalListOfMarca,
                    Type = TypeSearch.Match
                }
            };

            var resultSearch = this.processSearch.Filter(filters, new Pagination(1, 50));

            Assert.NotEmpty(resultSearch.ProcessoList);
        }
    }
}
