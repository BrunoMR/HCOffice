using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Natureza:")]
    public class OldLineNatureza : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var naturezaMatch = RegularExpressions.OldNaturezaRegex.Match(line);
            if (naturezaMatch.Success)
            {
                if (processo.Marca == null)
                    processo.Marca = new Marca();
                processo.Marca.Natureza = naturezaMatch.Groups[1].Value;
            }
        }
    }
}
