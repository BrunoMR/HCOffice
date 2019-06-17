namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "Nome", Name = "procurador")]
    public class ProcuradorIndex
    {
        public string Nome { get; set; }
    }
}
