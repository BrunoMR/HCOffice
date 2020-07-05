namespace ExtratorDeDados.Importer
{
    using System.Xml.Linq;
    using BusinessLayer;
    using DTOLayer;
    using Utils;

    public class ImportXml
    {
        private static XElement _xDocumento;

        public static RpiImported Deserializar_Processo(string path)
        {
            _xDocumento = XElement.Load(path);
            var rpi = _xDocumento.Deserializar<RpiImported>();

            DespachoNegocio.BuildAdditionalTextOfDespacho(rpi);
            DespachoNegocio.BuildPetitionOfDespacho(rpi);
            TitularNegocio.RemoveSecondTitular(rpi);

            return rpi;
        }   
    }
}