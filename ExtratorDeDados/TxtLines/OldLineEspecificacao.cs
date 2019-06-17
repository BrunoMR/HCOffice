using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Especificação:")]
    public class OldLineEspecificacao : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var especificacaoMatch = RegularExpressions.OldEspecificacaoRegex.Match(line);
            if (especificacaoMatch.Success)
            {
                if (processo.ClasseNacional == null)
                    processo.ClasseNacional = new ClasseNacional();

                processo.ClasseNacional.Descricao = especificacaoMatch.Groups[1].Value;
            }
        }
    }
}
