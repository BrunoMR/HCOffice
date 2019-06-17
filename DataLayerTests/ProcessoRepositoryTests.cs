using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DTOLayer;
using Xunit;

namespace DataLayerTests
{
    public class ProcessoRepositoryTests    
    {
        IProcessoRepository _processoRepository = new ProcessoRepository();

        [Fact]
        public async Task Should_retrieve_process_by_number()
        {
            const string numero = "820864757";

            var result = await _processoRepository.FindByNumeroProcesso(numero);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_retrieve_all_of_process_by_number()
        {
            const string numero = "901385166"; // "840278403";

            var result = await _processoRepository.GetAllOfProcessoByNumeroAsync(numero);

            Assert.NotNull(result);
        }
    }
}
