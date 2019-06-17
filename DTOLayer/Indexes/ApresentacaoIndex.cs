namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "ApresentacaoDescricao", Name = "apresentacao")]
    public class ApresentacaoIndex
    {
        public string ApresentacaoDescricao { get; set; }
    }
}
