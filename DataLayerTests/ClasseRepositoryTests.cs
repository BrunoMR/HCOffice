namespace DataLayerTests
{
    using Xunit;
    using DTOLayer;
    using DataLayer;
    public class ClasseRepositoryTests
    {
        readonly IClasseRepository _classeRepository = new ClasseRepository();

        [Fact]
        public void Should_find_by_code()
        {
            const string code = "N0843";
            var result = _classeRepository.FindByCodeClasse(code);

            Assert.Equal("Serviços de fornecimento de comida e bebida; acomodações temporárias.", result.Descricao);
        }

        [Fact]
        public void Should_not_find_by_code()
        {
            const string code = "N9999";
            var result = _classeRepository.FindByCodeClasse(code);

            Assert.Null(result);
        }

        [Fact]
        public void Should_get_all_items()
        {
            var result = _classeRepository.GetAll();

            Assert.NotNull(result);
        }
    }
}
