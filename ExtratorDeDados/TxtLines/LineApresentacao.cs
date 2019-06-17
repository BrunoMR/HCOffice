using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("Apres.:")]
    public class LineApresentacao : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var apresentacaoMatch = RegularExpressions.ApresentacaoLineRegex.Match(line);
            if (apresentacaoMatch.Success)
            {
                processo.Marca = new Marca()
                {
                    Apresentacao = apresentacaoMatch.Groups[1].Value,
                    Natureza = apresentacaoMatch.Groups[2].Value
                };
            }
        }
    }
}
