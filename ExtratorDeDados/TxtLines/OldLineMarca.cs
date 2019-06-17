using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Marca:")]
    public class OldLineMarca : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var marcaMatch = RegularExpressions.OldMarcaRegex.Match(line);
            if (marcaMatch.Success)
            {
                processo.Marca = new Marca()
                {
                    Nome = marcaMatch.Groups[1].Value
                };
            }
        }
    }
}
