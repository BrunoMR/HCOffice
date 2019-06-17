namespace DTOLayer
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ProtocoloImported
    {
        [XmlAttribute("numero")]
        public string Numero { get; set; }   

        [XmlAttribute("data")]
        public string Data { get; set; }

        [XmlAttribute("codigoServico")]
        public string CodigoServico { get; set; }

        [XmlElement("requerente")]
        public Requerente Requerente { get; set; }

        [XmlElement("procurador")]
        public string Procurador { get; set; }

        [XmlElement("cessionario")]
        public CessionarioImported Cessionario { get; set; }
    }
}
