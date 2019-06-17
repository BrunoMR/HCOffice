using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class Prioridades
    {
        private Prioridade _prioridade;

        [XmlElement("prioridade")]
        public Prioridade Prioridade
        {
            get { return _prioridade; }
            set { _prioridade = value; }
        }
    }
}
