namespace DTOLayer
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class CessionarioImported
    {
        [XmlAttribute("nome-razao-social")]
        public string NomeRazaoSocial { get; set; }
        
    }
}
