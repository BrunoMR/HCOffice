using ExtratorDeDados.Importer;
using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Processo:")]
    public class OldLineProcesso : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var processoMatch = RegularExpressions.OldProcessoRegex.Match(line);
            if (processoMatch.Success)
            {
                ImportTxt.NewProcesso = true;
                processo = new ProcessoImported()
                {
                    NumeroProcesso = processoMatch.Groups[1].Value
                };
            }
        }
    }
}
