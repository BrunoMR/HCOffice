using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class Titular
    {
        [XmlAttribute("nome-razao-social")]
        public string Nome { get; set; }

        [XmlAttribute("pais")]
        public string Pais { get; set; }

        [XmlAttribute("uf")]
        public string Uf { get; set; }

        [XmlIgnore]
        public string CpfCnpj { get; set; }
    }
}