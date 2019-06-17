using System.Collections.Generic;
using System.Linq;
using DTOLayer;
using BusinessLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("¢Classe:")]
    public class OldLineClasse : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var classeMatch = RegularExpressions.OldClasseRegex.Match(line);
            if (classeMatch.Success)
            {
                processo.ClasseNacional = new ClasseNacional()
                {
                    Codigo = classeMatch.Groups[2].Value,
                    SubClassesNacional = new SubClassesNacional()
                    {
                        SubClasseNacionais = new List<SubClasseNacional>()
                    }
                };

                var splitClasses = classeMatch.Groups[3].Value.Split('.');
                processo
                    .ClasseNacional
                    .SubClassesNacional
                    .SubClasseNacionais
                    .AddRange(splitClasses.Select(x => new SubClasseNacional()
                    {
                        Codigo = x
                    }));
            }
            else
            {
                classeMatch = RegularExpressions.OldNclRegex.Match(line);
                if (classeMatch.Success)
                {
                    processo.ClasseNice = new ClasseNice()
                    {
                        Codigo = ClasseNegocio.BuildCodeClasseNiceOldTxt(classeMatch.Groups)
                    };
                }
            }
        }
    }
}
