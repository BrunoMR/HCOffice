using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Concessão:")]
    public class OldLineDataConcessao : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var dataConcessaoMatch = RegularExpressions.OldConcessaoRegex.Match(line);
            if (dataConcessaoMatch.Success)
            {
                if (processo == null)
                    processo = new ProcessoImported();
                processo.DataConcessao = dataConcessaoMatch.Groups[1].Value;
            }
        }
    }
}
