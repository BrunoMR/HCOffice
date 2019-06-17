using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("Marca:")]
    public class LineMarca : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var marcaMatch = RegularExpressions.MarcaLineRegex.Match(line);
            if (marcaMatch.Success)
            {
                if (processo.Marca == null)
                    processo.Marca = new Marca();
                processo.Marca.Nome = marcaMatch.Groups[1].Value;
            }
        }
    }
}
