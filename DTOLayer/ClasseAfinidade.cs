namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    [Alias("CLASSE_AFINIDADE")]
    public class ClasseAfinidade
    {
        [Alias("NUMERO_CLASSE_A")]
        [References(typeof(Classe))]
        public string ClasseAId { get; set; }

        [Reference]
        public Classe ClasseA { get; set; }

        [Alias("NUMERO_CLASSE_B")]
        [References(typeof(Classe))]
        public string ClasseBId { get; set; }

        [Reference]
        public Classe ClasseB { get; set; }
    }
}
