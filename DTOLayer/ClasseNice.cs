using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class ClasseNice
    {
        private string _codigo;
        private string _descricao;
        private string _status;

        [XmlAttribute("codigo")]
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        [XmlElement("especificacao")]
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        [XmlElement("status")]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}