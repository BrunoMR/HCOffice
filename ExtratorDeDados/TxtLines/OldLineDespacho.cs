using System.Collections.Generic;
using System.Text.RegularExpressions;
using DTOLayer;
using BusinessLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    public class OldLineDespacho : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            if (!RegularExpressions.OldStartsLineDespachoRegex.IsMatch(line))
                return;

            var pattern = string.Concat(@"^¢(\d{2}\/\d{2}\/\d{4})[\s+]", RpiNegocio.CurrentRpi.NumeroRpi, @"[\s+](\w{1,100})");
            var despachoMatch = new Regex(pattern).Match(line);
            if (despachoMatch.Success)
            {
                var textoComplementarMatch = RegularExpressions.OldTextoComplementarRegex.Match(line);

                var despachos = processo.Despachos ?? new Despachos() {Despacho = new List<DespachoImported>()};

                despachos.Despacho.Add(new DespachoImported()
                {
                    Codigo = despachoMatch.Groups[2].Value,
                    Complemento = textoComplementarMatch.Success ? textoComplementarMatch.Groups[1].Value : null
                });

                processo.Despachos = despachos;
            }
        }
    }
}
