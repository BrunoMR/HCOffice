using DTOLayer;
using BusinessLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("NCL(")]
    public class LineNcl : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var nclMatch = RegularExpressions.NclLineRegex.Match(line);
            if (nclMatch.Success)
            {
                processo.ClasseNice = new ClasseNice()
                {
                    Codigo = ClasseNegocio.BuildCodeClasseNice(nclMatch.Groups),
                    Descricao = nclMatch.Groups[4].Value
                };
            }
        }
    }
}
