namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    public class Protocolo
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string Numero { get; set; }   

        public string Data { get; set; }

        [Alias("CODIGO_SERVICO")]
        public string CodigoServico { get; set; }

        [Alias("NOME_RAZAO_SOCIAL")]
        public string NomeRazaoSocial { get; set; }
        
        public string Pais { get; set; }

        public string Uf { get; set; }
    }
}
