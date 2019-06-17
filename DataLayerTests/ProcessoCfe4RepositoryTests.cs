namespace DataLayerTests
{
    using DataLayer;
    using Xunit;
    using System.Threading.Tasks;

    public class ProcessoCfe4RepositoryTests
    {
        readonly IProcessoCfe4Repository _processoCfe4Repository = new ProcessoCfe4Repository();

        [Fact]
        public async Task Should_retrieve_processo_despacho_by_id()
        {
        
            const int id = 2049647;

            var result = await _processoCfe4Repository.ProcessoCfe4LoadByProcessoIdAsync(id);

            Assert.NotNull(result);
            Assert.All(result, x => Assert.False(string.IsNullOrWhiteSpace(x.Cfe4.CodigoCfe4)));
        }
    }
}
