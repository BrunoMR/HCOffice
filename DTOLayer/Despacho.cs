namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    public class Despacho
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string Codigo { get; set; }

        [References(typeof(TipoDespacho))]
        public char? Tipo { get; set; }

        [Reference]
        public TipoDespacho TipoDespacho { get; set; }

        public string Descricao { get; set; }

        [Alias("DESCRICAO_INGLES")]
        public string DescricaoIngles { get; set; }

        [Alias("DESCRICAO_COMPLETA")]
        public string DescricaoCompleta { get; set; }

        [Alias("DESCRICAO_COMPLETA_INGLES")]
        public string DescricaoCompletaIngles { get; set; }

        [References(typeof(TipoSituacao))]
        public int? Situacao { get; set; }

        [Reference]
        public TipoSituacao TipoSituacao { get; set; }

        [Alias("PRAZO_1_EM_DIAS")]
        public int? Prazo1EmDias { get; set; }

        [Alias("PRAZO_2_EM_DIAS")]
        public int? Prazo2EmDias { get; set; }
    }
}
