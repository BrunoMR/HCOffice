using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Apostila:")]
    public class OldLineApostila : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var apostilaMatch = RegularExpressions.OldApostilaRegex.Match(line);
            if (apostilaMatch.Success)
            {
                processo.Apostila = apostilaMatch.Groups[1].Value;
            }
        }
    }
}
