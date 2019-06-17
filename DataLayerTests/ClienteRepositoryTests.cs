using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Xunit;

namespace DataLayerTests
{
    public class ClienteRepositoryTests
    {
        readonly IClienteRepository _clienteRepository = new ClienteRepository();

        [Fact]
        public async Task Should_find_by_Id_Load()
        {
            const int id = 1;

            var result = await _clienteRepository.FindByIdLoadAsync(id);

            Assert.NotNull(result.TipoPessoa);
        }
    }
}
