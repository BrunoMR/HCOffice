using System;
using System.Linq;
using System.Text;
using DTOLayer.Enum;
using Utils;

namespace BusinessLayerTests
{
    using System.Threading.Tasks;
    using AutoMapper;
    using BusinessLayer;
    using Xunit;
    using System.Collections.Generic;
    using DTOLayer.Indexes;

    public class SyncElasticTests
    {
        readonly ISyncElastic _syncElastic = new SyncElastic();

        [Fact]
        public void Should_syncronization()
        {
            //IProcessoNegocio processoNegocio = new ProcessoNegocio();
            _syncElastic.SynchronizationProcessesByAllRpis();
            //_syncElastic.SynchronizationOfAllDatabase();

            //var listProcessos = await processoNegocio.GetAll();

            //Mapper.CreateMap<Processo, ProcessoIndex>().IgnoreAllNonExisting();

            //var listProcessoIndexs = Mapper.Map<List<Processo>>(listProcessos);

            //var result = cfe4Repository.Add(cfe4);

            //Assert.NotNull(result);
        }
        
        [Fact]
        public void Should_syncronization_of_processes_by_rpi()
        {
            var result = _syncElastic.SynchronizationProcessesByRpi(2001, 2001);

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_procuradores()
        {
            //var opa = "2ele".Phonetic();
            //var yesNoList = Enum.GetValues(typeof(YesNo))
            //                .Cast<object>()
            //                .ToDictionary(item => (short)item, item => Enum.GetName(typeof(YesNo), item));

            var result = _syncElastic.SynchronizationOfAllProcuradores();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_titulares()
        {
            var result = _syncElastic.SynchronizationOfAllTitulares();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_syncronization_of_classes()
        {
            var result = _syncElastic.SynchronizationOfAllClasses();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_cfe4s()
        {
            var result = _syncElastic.SynchronizationOfAllCfe4S();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_despachos()
        {
            var result = _syncElastic.SynchronizationOfAllDespachos();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_classeAfinidades()
        {
            var result = _syncElastic.SynchronizationOfAllClasseAfinidades();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_presentations()
        {
            var result = _syncElastic.SynchronizationOfAllPresentations();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_countries()
        {
            var result = _syncElastic.SynchronizationOfAllCountries();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_states()
        {
            var result = _syncElastic.SynchronizationOfAllStates();

            Assert.True(result);
        }

        [Fact]
        public void Should_syncronization_of_athributes()
        {
            var result = _syncElastic.SynchronizationOfAllAtrhibutes();

            Assert.True(result);
        }

        // Criar testes para testar os groupby como de classes nacionais
        // var teste = auxClasseList.Where(x => x.ProcessoNumero == "812557204").ToList();
    }
}
