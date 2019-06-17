namespace DataLayerTests
{
    using DataLayer;
    using Xunit;
    using System.Threading.Tasks;

    public class ProcessoDespachoRepositoryTests
    {
        readonly IProcessoDespachoRepository _processoDespachoRepository = new ProcessoDespachoRepository();

        [Fact]
        public async Task Should_retrieve_processo_despacho_by_processo_id()
        {
            const int processoId = 2049636;

            var result = await _processoDespachoRepository.ProcessoDespachoLoadByProcessoIdAsync(processoId);

            Assert.NotNull(result);
        }
    }
}
