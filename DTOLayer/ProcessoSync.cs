namespace DTOLayer
{
    using System;
    using Nest;

    public class ProcessoSync
    {
        public int? Id { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public string CpfCnpjInpi { get; set; }
        public string TitularPais { get; set; }
        public string TitularPaisNome { get; set; }
        public string TitularUf { get; set; }
        public string TitularUfNome { get; set; }
        public string Procurador { get; set; }
        public string Marca { get; set; }

        [String(Ignore = true)]
        public string MarcaOrtografada { get; set; }

        public string MarcaNaoOrtografada { get; set; }
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
        public string ClasseInternacional { get; set; }
        public string ClasseInternacionalEdicao { get; set; }
        public string ClasseInternacionalDescricao { get; set; }
        public string Classe1 { get; set; }
        public string Classe1Sub { get; set; }
        public string Classe1SubDescricao { get; set; }
        public string Classe2 { get; set; }
        public string Classe2Sub { get; set; }
        public string Classe2SubDescricao { get; set; }
        public string Classe3 { get; set; }
        public string Classe3Sub { get; set; }
        public string Classe3SubDescricao { get; set; }
        public string Cfe4 { get; set; }
        public string Cfe4Descricao { get; set; }
        public string DespachoCodigo { get; set; }
        public string DespachoDescricao { get; set; }
        public string DespachoDescricaoCompleta { get; set; }
        public int DespachoRpi { get; set; }
        public DateTime DespachoData { get; set; }
        public string DespachoComplemento { get; set; }
        public string DespachoSituacao { get; set; }
        public string DespachoTipo { get; set; }
        public string DespachoTipoDescricao { get; set; }
        public string ProtocoloNumero { get; set; }
        public string ProtocoloCodigo { get; set; }
        public DateTime? ProtocoloData { get; set; }
        public string ProtocoloNomeRazaoSocial { get; set; }
        public string ProtocoloPais { get; set; }
        public string ProtocoloUf { get; set; }
    }
}
