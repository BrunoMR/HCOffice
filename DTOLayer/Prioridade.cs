using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class Prioridade
    {
        private string _numero;
        private string _pais;
        private string _data;
        
        [XmlAttribute("numero")]
        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        [XmlAttribute("pais")]
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        [XmlAttribute("data")]
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
