using System.Threading.Tasks;
using BusinessLayer;
using Utils;
using Xunit;

namespace BusinessLayerTests
{
    public class ProcessoNegocioTests
    {
        readonly IProcessoNegocio _processoNegocio = new ProcessoNegocio();

        [Fact]
        public async Task Should_retrieve_process_by_number()
        {
            const string numero = "901385166";

            var result = await _processoNegocio.GetAllOfProcessoByNumeroAsync(numero);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_build_full_processo()
        {
            var result = await _processoNegocio.GetAllFullProcessoAsync();

            Assert.NotNull(result);
            Assert.All(result, p => Assert.False(string.IsNullOrWhiteSpace(p.Numero)));
            Assert.All(result, p=> Assert.NotNull(p.ProcessoDespachoList));
        }

        [Fact]
        public void Should_marca_nao_ortografar_should_be_ok()
        {
            var result = "ANnCHOR STRrANDEDD COTTON".NotSpellManyWords();

            Assert.NotNull(result);
            Assert.Equal(result, "ANCHOR STRANDED COTON");
        }

    }
}
