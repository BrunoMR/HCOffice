namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "Codigo", Name = "cfe4")]
    public class Cfe4Index
    {
        public string Codigo { get; set; }
    }
}
