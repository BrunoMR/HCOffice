namespace DTOLayer.Indexes
{
    using Nest;
    using System.Collections.Generic;

    [ElasticsearchType(IdProperty = "ClasseA", Name = "classeAfinidade")]
    public class ClasseAfinidadeIndex
    {
        public string ClasseA { get; set; }
        public List<string> ClasseAfinidade { get; set; }
    }
}
