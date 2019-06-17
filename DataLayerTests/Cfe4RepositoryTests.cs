using DataLayer;
using DTOLayer;
using Xunit;

namespace DataLayerTests
{
    public class Cfe4RepositoryTests
    {
        [Fact]
        public void Should_insert_cfe4()
        {
            ICfe4Repository cfe4Repository = new Cfe4Repository();

            var cfe4 = new CFE4()
            {
                CodigoCfe4 = "99.99.99",
                Descricao = "testando insert"
            };

            var result = cfe4Repository.Add(cfe4);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_findById_cfe4()
        {
            ICfe4Repository cfe4Repository = new Cfe4Repository();

            const int id = 4980;

            var result = cfe4Repository.FindById(id);

            Assert.Equal("99.99.99", result.CodigoCfe4);
        }

        [Fact]
        public void Should_remove_cfe4()
        {
            ICfe4Repository cfe4Repository = new Cfe4Repository();

            const int id = 4980;
            cfe4Repository.Remove(id);

            var result = cfe4Repository.FindById(id);

            Assert.Null(result);
        }

        [Fact]
        public void Should_have_cfe4()
        {
            ICfe4Repository cfe4Repository = new Cfe4Repository();

            var cfe4List = cfe4Repository.GetAll();

            Assert.NotNull(cfe4List);
        }
    }
}
