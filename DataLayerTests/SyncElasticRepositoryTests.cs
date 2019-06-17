using System.Threading.Tasks;
using DataLayer;
using Xunit;

namespace DataLayerTests
{
    public class SyncElasticRepositoryTests
    {
        readonly ISyncElasticRepository _syncElasticRepository = new SyncElasticRepository();

        [Fact]
        public void Should_retrieve_processes_to_sync()
        {
            const int start = 22;
            const int end = 50;

            var result = _syncElasticRepository.GetInRange(start, end);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_retrieve_processes_byRpi_to_sync()
        {
            const int start = 3004;
            const int end = 3004;

            var result = _syncElasticRepository.GetFullProcessosByRpi(start, end);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_total_lines_toSync()
        {
            var result = _syncElasticRepository.GetCountToSync();

            Assert.NotNull(result);
            Assert.True(result > 0);
        }

        [Fact]
        public void Should_get_all_procuradores_toSync()
        {
            var result = _syncElasticRepository.GetAllProcuradores();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_all_titulares_toSync()
        {
            var result = _syncElasticRepository.GetAllTitulares();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_all_classes_toSync()
        {
            var result = _syncElasticRepository.GetAllClasses();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_all_cfe4s_toSync()
        {
            var result = _syncElasticRepository.GetAllCfe4S();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_all_despachos_toSync()
        {
            var result = _syncElasticRepository.GetAllDespachos();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_all_classeAfinidades_toSync()
        {
            var result = _syncElasticRepository.GetAllClassSimilarities();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_all_presentations_toSync()
        {
            var result = _syncElasticRepository.GetAllPresentations();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_all_processo_Ids_toSync()
        {
            var result = _syncElasticRepository.GetAllIdsOfProcesso();

            Assert.NotNull(result);
        }
    }
}
