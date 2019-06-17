using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("Prior.:")]
    public class LinePrioridade : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            if (!line.StartsWith("Prior.:"))
                return;

            var prioridadeMatch = RegularExpressions.PrioridadeLineRegex.Match(line);
            if (prioridadeMatch.Success)
            {
                processo.Prioridades = new Prioridades()
                {
                    Prioridade = new Prioridade()
                    {
                        Numero = prioridadeMatch.Groups[1].Value,
                        Data = prioridadeMatch.Groups[2].Value,
                        Pais = prioridadeMatch.Groups[3].Value
                    }
                };
            }
        }
    }
}
