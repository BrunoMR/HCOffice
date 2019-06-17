namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    public class Atributo
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        
    }
}
