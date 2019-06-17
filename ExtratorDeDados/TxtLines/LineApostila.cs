using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("Apostila:")]
    public class LineApostila : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            if (!line.StartsWith("Apostila:"))
                return;

            var apostilaMatch = RegularExpressions.ApostilaLineRegex.Match(line);
            if (apostilaMatch.Success)
            {
                processo.Apostila = apostilaMatch.Groups[1].Value;
            }
        }
    }
}
