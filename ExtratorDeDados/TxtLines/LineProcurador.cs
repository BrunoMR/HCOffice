using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("Procurador:")]
    public class LineProcurador : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var procuradorMatch = RegularExpressions.ProcuradorLineRegex.Match(line);
            if (procuradorMatch.Success)
            {
                processo.Procurador = procuradorMatch.Groups[1].Value;
            }
        }
    }
}
