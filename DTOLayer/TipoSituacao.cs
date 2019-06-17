namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    [Alias("TIPO_SITUACAO")]
    public class TipoSituacao
    {
        [PrimaryKey]
        public int? Tipo { get; set; }
        public string Descricao { get; set; }
    }
}
