namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    public class Uf
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        [Alias("NOME_INGLES")]
        public string NomeIngles { get; set; }
    }
}
