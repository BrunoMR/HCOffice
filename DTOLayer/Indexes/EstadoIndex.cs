namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "EstadoSigla", Name = "estado")]
    public class EstadoIndex
    {
        public string EstadoSigla { get; set; }
        public string EstadoNome { get; set; }
    }
}
