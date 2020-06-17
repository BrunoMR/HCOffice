using System;
using System.Xml.Serialization;

namespace DTOLayer
{
    [Serializable]
    public class DadosDeMadri
    {
        [XmlAttribute("numero-inscricao-internacional")]
        public string NumeroInscricaoInternacional { get; set; }

        [XmlAttribute("data-recebimento-inpi")]
        public string DataRecebimentoInpi { get; set; }
    }
}