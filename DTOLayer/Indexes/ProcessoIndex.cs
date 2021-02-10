namespace DTOLayer.Indexes
{
    using System;
    using System.Collections.Generic;
    using Nest;

    [ElasticsearchType(IdProperty = "Numero", Name = "processo")]
    public class ProcessoIndex
    {
        public int? Id { get; set; }
        public string Numero { get; set; }

        //TODO: Removed holder properties because now there is a list with holders
        public string Titular { get; set; }
        public string CpfCnpjInpi { get; set; }
        public string TitularPais { get; set; }
        public string TitularPaisNome { get; set; }
        public string TitularUf { get; set; }
        public string TitularUfNome { get; set; }

        public string Procurador { get; set; }
        public string Marca { get; set; }

        //[String(Ignore = true)]
        public string MarcaOrtografada { get; set; }
        public string MarcaOrtografadaInverted { get; set; }
        public string MarcaNaoOrtografada { get; set; }
        public string MarcaNaoOrtografadaNoSpace { get; set; }
        public string MarcaNaoOrtografadaNoSpaceInverted { get; set; }
        public string MarcaNaoOrtografadaInverted { get; set; }
        public string MarcaSemVogais { get; set; }

        public string Prioridade { get; set; }
        public DateTime? PrioridadeData { get; set; }
        public string PrioridadePais { get; set; }
        public string Especificacao { get; set; }
        public string Apostila { get; set; }

        public DateTime? DataDeposito { get; set; }
        public DateTime? DataConcessao { get; set; }
        public DateTime? DataRegistro { get; set; }
        public DateTime? DataVigencia { get; set; }
        public DateTime? DataOrdinarioInicial { get; set; }
        public DateTime? DataOrdinarioFinal { get; set; }
        public DateTime? DataExtraOrdinarioInicial { get; set; }
        public DateTime? DataExtraOrdinarioFinal { get; set; }

        public string Apresentacao { get; set; }
        public string Natureza { get; set; }
        public List<ClasseSync> Classe { get; set; }
        public List<Cfe4Sync> Cfe4 { get; set; }
        public List<DespachoSync> Despacho { get; set; }
        public List<TitularSync> Titulares { get; set; }
        public Pagination Paginacao { get; set; }
    }
}