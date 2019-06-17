using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class ClasseVienna
    {
        private int _edicao;
        private List<CFE4> _cfe4s;

        [XmlElement("classe-vienna")]
        public List<CFE4> Cfe4S
        {
            get { return _cfe4s; }
            set { _cfe4s = value; }
        }

        [XmlAttribute("edicao", DataType = "int")]
        public int Edicao
        {
            get { return _edicao; }
            set { _edicao = value; }
        }
    }
}
