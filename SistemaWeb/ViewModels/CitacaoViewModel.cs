namespace SistemaWeb.ViewModels
{
    using System.Collections.Generic;
    using DTOLayer.Indexes;

    public class CitacaoViewModel
    {
        public string Numero { get; set; }
        public string DataDeposito { get; set; }
        public string Marca { get; set; }
        public string Classe { get; set; }
        public List<DespachoSync> Despacho { get; set; }
        public string Apresentacao { get; set; }
        public string Titular { get; set; }
    }
}
