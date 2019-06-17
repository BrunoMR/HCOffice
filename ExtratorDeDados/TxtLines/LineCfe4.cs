using System;
using System.Collections.Generic;
using System.Linq;
using DTOLayer;
using Utils;
using WebGrease.Css.Extensions;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("CFE(")]
    public class LineCfe4 : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var cfe4Match = RegularExpressions.CfeLineRegex.Match(line);
            if (cfe4Match.Success)
            {
                processo.ClasseVienna = new ClasseVienna()
                {
                    Edicao = Convert.ToInt32(cfe4Match.Groups[2].Value),
                    Cfe4S = new List<CFE4>()
                };

                var cfe4List = new List<string>();

                cfe4Match.Groups[3].Value.Split(new char[] { ';' , ','}).ForEach(firstSplit =>
                {
                    var virgulaSplit = firstSplit.Split(',');
                    var tracoSplit = firstSplit.Split('-');

                    var existsTracoInVirgula = false;
                    if (firstSplit.IndexOf(',') > 0)
                        existsTracoInVirgula = virgulaSplit.Any(x => x.IndexOf('-') > 0);

                    if (virgulaSplit.Length > 1)
                    {
                        if (existsTracoInVirgula)
                        {
                            virgulaSplit.ForEach(comma =>
                            {
                                var tracoInVirgula = comma.Split('-');
                                cfe4List.Add(tracoInVirgula.First());

                                var startsOfCfe4Virgula = comma
                                            .Substring(0, comma.LastIndexOf(".", StringComparison.Ordinal) + 1);

                                var restOfComma = tracoInVirgula.Where(x => x != tracoInVirgula.First()).ToArray();
                                cfe4List.AddRange(restOfComma.Select(x => startsOfCfe4Virgula + x));
                            });
                        }
                        else
                        {
                            var startsOfCfe4 = virgulaSplit
                                            .First()
                                            .Substring(0, virgulaSplit.First().LastIndexOf(".", StringComparison.Ordinal) + 1);

                            cfe4List.Add(virgulaSplit.First());

                            virgulaSplit = virgulaSplit.Where(x => x != virgulaSplit.First()).ToArray();
                            cfe4List.AddRange(virgulaSplit.Select(x => startsOfCfe4 + x));
                        }
                    }
                    else if (existsTracoInVirgula == false && (tracoSplit.Length > 1))
                    {
                        var startsOfCfe4 = tracoSplit
                                           .First()
                                           .Substring(0, tracoSplit.First().LastIndexOf(".", StringComparison.Ordinal) + 1);

                        cfe4List.Add(tracoSplit.First());

                        tracoSplit = tracoSplit.Where(x => x != tracoSplit.First()).ToArray();
                        cfe4List.AddRange(tracoSplit.Select(x => startsOfCfe4 + x));
                    }
                    else if (!string.IsNullOrWhiteSpace(firstSplit))
                    {
                        cfe4List.Add(firstSplit);
                    }

                });

                processo.ClasseVienna
                    .Cfe4S
                    .AddRange(cfe4List
                        .Select(x => new CFE4()
                        {
                            CodigoCfe4 = x.Trim()
                        })
                    );
            }
        }
    }
}
