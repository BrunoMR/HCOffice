namespace ExtratorDeDados.TxtLines
{
    using System.Collections.Generic;
    using DTOLayer;
    using Utils;

    [ImportTxt("Tit.")]
    public class LineTitular : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            Titular titular = null;

            var titularMatch = RegularExpressions.TitularNomeLineRegex.Match(line);

            if (titularMatch.Success)
            {
                titular = new Titular()
                {
                    Nome = titularMatch.Groups[1].Value
                };
            }

            var titularPaisEstado = RegularExpressions.TitularPaisEEstadoLineRegex.Match(line);
            if (titularPaisEstado.Success)
            {
                if (titular == null)
                    titular = new Titular();

                if (string.IsNullOrWhiteSpace(titularPaisEstado.Groups[3].Value))
                {
                    titular.Pais = titularPaisEstado.Groups[1].Value;
                    titular.Uf = titularPaisEstado.Groups[2].Value;
                }
                else
                {
                    titular.Pais = titularPaisEstado.Groups[3].Value;
                }
            }

            if (titular != null)
                processo.Titulares = new Titulares()
                {
                    Titular = new List<Titular>
                                  {
                                      titular
                                  }
                };
        }
    }
}
