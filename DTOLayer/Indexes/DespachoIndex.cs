namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "Codigo", Name = "despacho")]
    public class DespachoIndex
    {
        public string Codigo { get; set; }
    }
}
