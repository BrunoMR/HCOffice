namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "CodigoAtributo", Name = "atributo")]
    public class AtributoIndex
    {
        public string CodigoAtributo { get; set; }
        public string DescricaoAtributo { get; set; }
    }
}
