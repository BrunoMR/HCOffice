namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    [Alias("TIPO_DESPACHO")]
    public class TipoDespacho
    {
        [PrimaryKey]
        public char Tipo { get; set; }
        public string Descricao { get; set; }

        [Ignore]
        public bool IsNew { get; set; }
    }
}
