using System.ComponentModel.DataAnnotations;

namespace DTOLayer
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class Processo
    {
        [AutoIncrement]
        public int? Id { get; set; }

        [Display(Name = "Processo")]
        public string Numero { get; set; }

        [Display(Name = "Titular")]
        [Alias("NOME_TITULAR")]
        public string NomeTitular { get; set; }

        [Display(Name = "CPF/CNPJ/N INPI")]
        [Alias("CPF_CNPJ_INPI_TITULAR")]
        public string CpfCnpjInpi { get; set; }

        [Display(Name = "Procurador")]
        [Alias("NOME_PROCURADOR")]
        public string NomeProcurador { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Alias("MARCA_ORTOGRAFADA")]
        public string MarcaOrtografada { get; set; }

        [Alias("MARCA_NAO_ORTOGRAFADA")]
        public string MarcaNaoOrtografada { get; set; }

        public string Prioridade { get; set; }

        [Alias("DATA_PRIORIDADE")]
        public DateTime DataPrioridade { get; set; }

        [Alias("NOME_PAIS_PRIORIDADE")]
        public string NomePaisPrioridade { get; set; }

        [Alias("TIPO_APRESENTACAO")]
        [References(typeof(TipoApresentacao))]
        public int? TipoApresentacaoId { get; set; }

        [Reference]
        public TipoApresentacao TipoApresentacao { get; set; }

        [Alias("TIPO_NATUREZA")]
        [References(typeof(TipoNatureza))]
        public int? TipoNaturezaId { get; set; }

        [Reference]
        public TipoNatureza TipoNatureza { get; set; }

        [Alias("CLASSE_INTERNACIONAL")]
        [References(typeof(Classe))]
        public string ClasseInternacionalId { get; set; }

        [Reference]
        public Classe ClasseInternacional { get; set; }

        [Alias("CLASSE_1")]
        [References(typeof(Classe))]
        public string Classe1Id { get; set; }

        [Reference]
        public Classe Classe1 { get; set; }

        [Alias("CLASSE_2")]
        [References(typeof(Classe))]
        public string Classe2Id { get; set; }

        [Reference]
        public Classe Classe2 { get; set; }

        [Alias("CLASSE_3")]
        [References(typeof(Classe))]
        public string Classe3Id { get; set; }

        [Reference]
        public Classe Classe3 { get; set; }

        [Display(Name = "Especificação da Classe")]
        public string Especificacao { get; set; }

        public string Apostila { get; set; }

        [Alias("NUMERO_REFERENCIA")]
        public string NumeroReferencia { get; set; }

        [Alias("DATA_DEPOSITO")]
        public DateTime DataDeposito { get; set; }

        [Alias("DATA_CONCESSAO")]
        public DateTime DataConcessao { get; set; }

        [Alias("DATA_REGISTRO")]
        public DateTime DataRegistro { get; set; }

        [Alias("DATA_VIGENCIA")]
        public DateTime DataVigencia { get; set; }

        [Alias("DATA_ORDINARIO_INICIAL")]
        public DateTime DataOrdinarioInicial { get; set; }

        [Alias("DATA_ORDINARIO_FINAL")]
        public DateTime DataOrdinarioFinal { get; set; }

        [Alias("DATA_EXTRA_ORDINARIO_INICIAL")]
        public DateTime DataExtraOrdinarioInicial { get; set; }

        [Alias("DATA_EXTRA_ORDINARIO_FINAL")]
        public DateTime DataExtraOrdinarioFinal { get; set; }

        [Reference]
        public List<ProcessoDespacho> ProcessoDespachoList { get; set; }

        [Reference]
        public List<ProcessoCfe4> ProcessoCfe4List { get; set; }
    }
}
