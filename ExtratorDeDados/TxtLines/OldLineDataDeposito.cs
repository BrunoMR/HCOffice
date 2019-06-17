using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Depósito:")]
    public class OldLineDataDeposito : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var dataDepositoMatch = RegularExpressions.OldDepositoRegex.Match(line);
            if (dataDepositoMatch.Success)
            {
                if (processo == null)
                    processo = new ProcessoImported();
                processo.DataDeposito = dataDepositoMatch.Groups[1].Value;
            }
        }
    }
}
