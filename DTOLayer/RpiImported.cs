namespace DTOLayer
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("revista")]
    public class RpiImported
    {
        [XmlAttribute("numero", DataType = "int")]
        public int NumeroRpi { get; set; }
     
        [XmlAttribute("data")]
        public string DataRpi { get; set; }

        [XmlElement("processo")]
        public List<ProcessoImported> Processo { get; set; }
    }
}