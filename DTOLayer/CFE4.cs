namespace DTOLayer
{
    using System;
    using System.Xml.Serialization;
    using ServiceStack.DataAnnotations;

    [Serializable()]
    public class CFE4 : IEntity
    {
        [XmlIgnore]
        [AutoIncrement]
        public int? Id { get; set; }

        [XmlAttribute("codigo")]
        [Alias("CODIGO_CFE4")]
        public string CodigoCfe4 { get; set; }

        public string Descricao { get; set; }

        [XmlIgnore]
        [Alias("DESCRICAO_INGLES")]
        public string DescricaoIngles { get; set; }
    }
}
