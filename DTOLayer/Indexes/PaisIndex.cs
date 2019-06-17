namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "PaisSigla", Name = "pais")]
    public class PaisIndex
    {
        public string PaisSigla { get; set; }
        public string PaisNome { get; set; }
    }
}
