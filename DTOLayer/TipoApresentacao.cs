namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    [Alias("TIPO_APRESENTACAO")]
    public class TipoApresentacao
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string Descricao { get; set; }

        [Alias("DESCRICAO_INGLES")]
        public string DescricaoIngles { get; set; }

        public int Ordem { get; set; }
    }
}
