using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class SubClassesNacional
    {
        private List<SubClasseNacional> _subClasseNacionais;

        [XmlElement("sub-classe-nacional")]
        public List<SubClasseNacional> SubClasseNacionais
        {
            get { return _subClasseNacionais; }
            set { _subClasseNacionais = value; }
        }
    }
}
