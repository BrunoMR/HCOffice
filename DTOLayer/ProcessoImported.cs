namespace DTOLayer
{
    using System;
    using System.Xml.Serialization;
    using Utils;

    [Serializable]
    public class ProcessoImported
    {
        [XmlAttribute("numero")]
        public string NumeroProcesso { get; set; }

        [XmlAttribute("data-deposito")]
        public string DataDeposito { get; set; }

        [XmlAttribute("data-concessao")]
        public string DataConcessao { get; set; }

        [XmlAttribute("data-vigencia")]
        public string DataVigencia { get; set; }

        [XmlElement("apostila")]
        public string Apostila { get; set; }

        [XmlElement("procurador")]
        public string Procurador { get; set; }

        [XmlElement("marca")]
        public Marca Marca { get; set; }

        [XmlElement("dados-de-madri")]
        public DadosDeMadri DadosDeMadri { get; set; }

        [XmlElement("despachos")]
        public Despachos Despachos { get; set; }

        [XmlElement("titulares")]
        public Titulares Titulares { get; set; }

        [XmlElement("prioridade-unionista")]
        public Prioridades Prioridades { get; set; }

        [XmlElement("classes-vienna")]
        public ClasseVienna ClasseVienna { get; set; }

        [XmlElement("classe-nacional")]
        public ClasseNacional ClasseNacional { get; set; }

        [XmlElement("classe-nice")]
        public ClasseNice ClasseNice { get; set; }

        [XmlElement("lista-classe-nice")]
        public ListaClasseNice ListaClasseNice { get; set; }

        [XmlIgnore]
        public string DataRegistro
        {
            get
            {
                if (DataVigencia == null || (DateTime)DataVigencia.VerificarData() == DateTime.MinValue)
                    return null;
                return Convert.ToDateTime(DataVigencia.VerificarData()).AddYears(-10).ToString();
            }
        }
    }
}