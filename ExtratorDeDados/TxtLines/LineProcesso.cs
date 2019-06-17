using System;
using System.Collections.Generic;
using System.Linq;
using ExtratorDeDados.Importer;
using DTOLayer;
using BusinessLayer;
using Utils;

namespace ExtratorDeDados.TxtLines
{
    [ImportTxt("No.")]
    public class LineProcesso : IImportTxt
    {
        public void ValidateLine(string line, ref ProcessoImported processo)
        {
            var processoMatch = RegularExpressions.ProcessoLineRegex.Match(line);
            if (processoMatch.Success)
            {
                DateTime dataVigenciaProcesso;
                DateTime dataDepositoProcesso;
                var dataConcessaoProcesso = ProcessoNegocio.ValidateDatesProcesso(processoMatch, out dataVigenciaProcesso, out dataDepositoProcesso);

                if (ImportTxt.rpiTxt.Processo.Any(x => x.NumeroProcesso == processoMatch.Groups[1].Value))
                    processo = ImportTxt.rpiTxt.Processo.Find(x => x.NumeroProcesso == processoMatch.Groups[1].Value);
                else
                {
                    ImportTxt.NewProcesso = true;
                    processo = new ProcessoImported();
                }

                processo.NumeroProcesso = processoMatch.Groups[1].Value;
                processo.DataDeposito = dataDepositoProcesso.ToString().SeDataMaiorQueMinimo();
                processo.DataConcessao = dataConcessaoProcesso.ToString().SeDataMaiorQueMinimo();
                processo.DataVigencia = dataVigenciaProcesso.ToString().SeDataMaiorQueMinimo();

                if (processo.Despachos == null)
                    processo.Despachos = new Despachos()
                    {
                        Despacho = new List<DespachoImported>()
                        {
                            new DespachoImported()
                            {
                                Codigo = processoMatch.Groups[3].Value
                            }
                        }
                    };
                else
                {
                    processo.Despachos.Despacho.Add(new DespachoImported()
                    {
                        Codigo = processoMatch.Groups[3].Value
                    });
                }

                LogProcess.UpdateCurrentAndLastProcess(processo);
            }
        }
        
    }
}
