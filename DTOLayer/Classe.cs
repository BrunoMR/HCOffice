namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    public class Classe
    {
        [Alias("NUMERO_CLASSE")]
        public string NumeroClasse { get; set; }

        public string Descricao { get; set; }

        [Alias("DESCRICAO_INGLES")]
        public string DescricaoIngles { get; set; }

        [Ignore]
        public bool IsNew { get; set; }
    }
}
