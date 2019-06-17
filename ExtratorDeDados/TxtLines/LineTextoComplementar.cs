using System.Linq;
using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("*")]
    public class LineTextoComplementar : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var complementoMatch = RegularExpressions.TextoComplementarLineRegex.Match(line);
            if (complementoMatch.Success)
            {
                // Pega o último Despacho adicionado para adicionar o complemento
                var lastOrDefault = processo.Despachos.Despacho
                    .LastOrDefault(x => x.Codigo != null);
                if (lastOrDefault != null)
                    lastOrDefault.Complemento = complementoMatch.Groups[1].Value;
            }
        }
    }
}
