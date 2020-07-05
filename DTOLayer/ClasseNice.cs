using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class ClasseNice
    {
        [XmlAttribute("codigo")]
        public string Codigo { get; set; }

        [XmlElement("especificacao")]
        public string Descricao { get; set; }

        [XmlElement("traducao-especificacao")]
        public string TraducaoEspecificacao { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}