using System;
using System.Collections.Generic;
using DTOLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("Clas.Prod")]
    public class LineClasseNacional : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var classeMatch = RegularExpressions.ClasseLineRegex.Match(line);
            if (classeMatch.Success)
            {
                var classeNacional = classeMatch.Groups[1].Value.Split('.')[0];

                processo.ClasseNacional = new ClasseNacional()
                {
                    Codigo = classeNacional,
                    SubClassesNacional = new SubClassesNacional()
                    {
                        SubClasseNacionais = new List<SubClasseNacional>()
                    }
                };
                //try
                //{
                    var splitClasses = classeMatch.Groups[1].Value.Split(';');
                    foreach (var linha in splitClasses)
                    {
                        processo.ClasseNacional.SubClassesNacional.SubClasseNacionais.Add(new SubClasseNacional()
                        {
                            Codigo = linha.Split('.')[1]
                        });
                    }
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception(ex.Message);
                //}
            }
        }
    }
}
