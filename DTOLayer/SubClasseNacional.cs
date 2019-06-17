using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class SubClasseNacional
    {
        private string _codigo;

        [XmlAttribute("codigo")]
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}
