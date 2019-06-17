namespace SistemaWeb.ViewModels
{
    using DTOLayer.Indexes;
    using System.Collections.Generic;

    public class DetailProcessoViewModel
    {
        public int? Id { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public string TitularPais { get; set; }
        public string TitularPaisNome { get; set; }
        public string TitularUf { get; set; }
        public string TitularUfNome { get; set; }
        public string CpfCnpjInpi { get; set; }
        public string Procurador { get; set; }
        public string Marca { get; set; }
        public string MarcaOrtografada { get; set; }
        public string Prioridade { get; set; }
        public string PrioridadeData { get; set; }
        public string PrioridadePais { get; set; }
        public string Especificacao { get; set; }
        public string Apostila { get; set; }
        public string DataDeposito { get; set; }
        public string DataConcessao { get; set; }
        public string DataRegistro { get; set; }
        public string DataVigencia { get; set; }
        public string DataOrdinarioInicial { get; set; }
        public string DataOrdinarioFinal { get; set; }
        public string DataExtraOrdinarioInicial { get; set; }
        public string DataExtraOrdinarioFinal { get; set; }
        public string Apresentacao { get; set; }
        public string Natureza { get; set; }
        public string Classe { get; set; }
        public string Cfe4 { get; set; }
        public string Cfe4Description { get; set; }
        public List<DespachoSync> Despacho { get; set; }
    }
}
