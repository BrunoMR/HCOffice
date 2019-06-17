using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class Requerente
    {
        private string _nome;
        private string _pais;
        private string _uf;

        [XmlAttribute("nome-razao-social")]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        [XmlAttribute("pais")]
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        [XmlAttribute("uf")]
        public string Uf
        {
            get { return _uf; }
            set { _uf = value; }
        }
        
    }
}
