namespace ExtratorDeDados.TxtLines
{
    using System.Collections.Generic;
    using System.Linq;
    using DTOLayer;
    using Utils;

    [ImportTxt("C.N.P.J.")]
    public class LineCpfCnpj : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var cpfCnpjMatch = RegularExpressions.CpfCnpjLineRegex.Match(line);
            if (cpfCnpjMatch.Success)
            {
                if (processo.Titulares == null)
                {
                    processo.Titulares = new Titulares()
                    {
                        Titular = new List<Titular>()
                                      {
                                          new Titular()
                                      }
                    };
                }

                processo.Titulares.Titular.First().CpfCnpj = cpfCnpjMatch.Groups[1].Value;
            }
        }
    }
}
