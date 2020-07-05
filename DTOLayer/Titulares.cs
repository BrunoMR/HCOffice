namespace DTOLayer
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Titulares
    {
        private List<Titular> _titular;

        /// <summary>
        /// Gets or sets the titular.
        /// </summary>
        [XmlElement("titular")]
        public List<Titular> Titular
        {
            get { return _titular; }
            set { _titular = value; }
        }
    }
}