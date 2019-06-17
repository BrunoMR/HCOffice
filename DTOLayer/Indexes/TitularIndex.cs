namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "Nome", Name = "titular")]
    public class TitularIndex
    {
        public string Nome { get; set; }
    }
}
