namespace DTOLayer
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class DespachoImported
    {
        [XmlAttribute("codigo")]
        public string Codigo { get; set; }
        
        [XmlAttribute("nome")]
        public string Descricao { get; set; }

        [XmlElement("protocolo")]
        public ProtocoloImported Protocolo { get; set; }

        [XmlElement("texto-complementar")]
        public string Complemento { get; set; }
    }
}
