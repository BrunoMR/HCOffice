namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    [Alias("TIPO_NATUREZA")]
    public class TipoNatureza
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string Descricao { get; set; }

        [Alias("DESCRICAO_INGLES")]
        public string DescricaoIngles { get; set; }

        public int Ordem { get; set; }
    }
}
