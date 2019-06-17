namespace SistemaWeb.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class FilterViewModel
    {
        public List<string> Numero { get; set; }
        public List<string> Titular { get; set; }
        public string CpfCnpjInpi { get; set; }
        public List<string> Procurador { get; set; }
        public string Marca { get; set; }
        public string MarcaOrtografada { get; set; }
        public string Prioridade { get; set; }
        public string PrioridadeData { get; set; }
        public string PrioridadePais { get; set; }
        public string Especificacao { get; set; }
        public string Apostila { get; set; }
        public int Rpi { get; set; }
        public DateTime? DataDepositoStart { get; set; }
        public DateTime? DataDepositoEnd { get; set; }
        public DateTime? DataConcessaoStart { get; set; }
        public DateTime? DataConcessaoEnd { get; set; }
        public DateTime? DataRegistroInicial { get; set; }
        public DateTime? DataRegistroFinal { get; set; }
        public DateTime? DataVigencia { get; set; }
        public DateTime? DataOrdinarioInicial { get; set; }
        public DateTime? DataOrdinarioFinal { get; set; }
        public DateTime? DataExtraOrdinarioInicial { get; set; }
        public DateTime? DataExtraOrdinarioFinal { get; set; }
        public List<string> Apresentacao { get; set; }
        public string Natureza { get; set; }
        public List<string> Classe { get; set; }
        public List<string> Cfe4 { get; set; }
        public List<string> Despacho { get; set; }
        public bool LastDespacho { get; set; }
        public string DespachoComplemento { get; set; }
        public List<string> Pais { get; set; }
        public List<string> Estado { get; set; }

        public bool Phonetic { get; set; }
        public bool Affinity { get; set; }
        public bool Extinct { get; set; }
        public bool OnlyExtinct { get; set; }
        public bool ShowImages { get; set; }

        public bool Prefix { get; set; }
        public bool Suffix { get; set; }
        public bool Radical { get; set; }

        public bool ExactBrand { get; set; }
        public bool ExactWord { get; set; }

        public bool WithSpace { get; set; }
    }
}
