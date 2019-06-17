namespace DataLayerTests
{
    using Xunit;
    using DTOLayer;
    using DataLayer;

    public class DespachoRepositoryTests
    {
        readonly IDespachoRepository _despachoRepository = new DespachoRepository();

        [Fact]
        public void Should_find_by_Id_All_Despacho()
        {
            const int id = 1016;

            var result = _despachoRepository.FindById(id);

            Assert.NotNull(result.TipoSituacao);
        }

        [Fact]
        public void Should_get_All_Despacho()
        {
            var result = _despachoRepository.GetAll();

            Assert.All(result, x => Assert.False(string.IsNullOrWhiteSpace(x.Codigo)));
        }
    }
}
