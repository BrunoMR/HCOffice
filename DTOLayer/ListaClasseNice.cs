namespace DTOLayer
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class ListaClasseNice
    {
        [XmlElement("classe-nice")]
        public List<ClasseNice> ClassesNice { get; set; }
    }
}