namespace DTOLayer.Indexes
{
    using Nest;

    [ElasticsearchType(IdProperty = "Classe", Name = "classe")]
    public class ClasseIndex
    {
        public string Classe { get; set; }
        public string SubClasse { get; set; }
        public string Edicao { get; set; }
    }
}
