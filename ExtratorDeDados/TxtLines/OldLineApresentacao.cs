using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Apresentação:")]
    public class OldLineApresentacao : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var apresentacaoMatch = RegularExpressions.OldApresentacaoRegex.Match(line);
            if (apresentacaoMatch.Success)
            {
                if (processo.Marca == null)
                    processo.Marca = new Marca();
                processo.Marca.Apresentacao = apresentacaoMatch.Groups[1].Value;
            }
        }
    }
}
