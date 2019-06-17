using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class Despachos
    {
        private List<DespachoImported> _despacho;

        /// <summary>
        /// Gets or sets the despacho.
        /// </summary>
        [XmlElement("despacho")]
        public List<DespachoImported> Despacho
        {
            get { return _despacho; }
            set { _despacho = value; }
        }
    }
}
