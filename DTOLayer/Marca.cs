using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class Marca
    {
        private string _nome;
        private string _apresentacao;
        private string _natureza;

        [XmlElement("nome")]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        [XmlAttribute("apresentacao")]
        public string Apresentacao
        {
            get { return _apresentacao; }
            set { _apresentacao = value; }
        }

        [XmlAttribute("natureza")]
        public string Natureza
        {
            get { return _natureza; }
            set { _natureza = value; }
        }
    }
}
