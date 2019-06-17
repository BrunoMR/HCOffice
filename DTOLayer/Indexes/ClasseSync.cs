namespace DTOLayer.Indexes
{
    using System.Collections.Generic;

    public class ClasseSync
    {
        public string ProcessoNumero { get; set; }
        public string Codigo { get; set; }
        public string Edicao { get; set; }
        public string Descricao { get; set; }
        public List<SubClasseSync> SubClasse { get; set; }
    }
}
