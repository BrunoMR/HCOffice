using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class ClasseNacional
    {
        private string _codigo;
        private string _descricao;
        private SubClassesNacional _subClassesNacional;

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

        [XmlElement("sub-classes-nacional")]
        public SubClassesNacional SubClassesNacional
        {
            get { return _subClassesNacional; }
            set { _subClassesNacional = value; }
        }
    }
}
