using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using AutoFactory;
using DTOLayer;
using BusinessLayer;
using ExtratorDeDados.TxtLines;
using Utils;

namespace ExtratorDeDados.Importer
{
    public class ImportTxt
    {
        public static bool NewProcesso;
        public static RpiImported rpiTxt;
        private static readonly IAutoFactory<IImportTxt> factory = Factory.Create<IImportTxt>();

        #region Public Methods

        public static RpiImported TxtImport(string path)
        {
            var rpiMatch = RpiMatch(path);

            if (Convert.ToInt32(rpiMatch.Groups[5].Value) >= 721 && Convert.ToInt32(rpiMatch.Groups[5].Value) <= 1380)
                return ReadOldTxtFile(path, rpiMatch);
            else
                return ReadTxtFile(path, rpiMatch);
        }

        #endregion Public Methods

        #region Private Methods
        private static RpiImported ReadTxtFile(string path, Match rpiMatch)
        {
            ProcessoImported processo;
            rpiTxt = BuildRpi(rpiMatch, out processo);
            RpiNegocio.CurrentRpi = rpiTxt;

            var lines = File.ReadAllLines(path, Encoding.UTF7);

            foreach (var line in lines)
            {
                var lineImport = factory.SeekPartFromAttribute<ImportTxtAttribute>(x => line.StartsWith(x.LineStart));
                lineImport.ValidateLine(line, ref processo);
                
                if (NewProcesso)
                {
                    rpiTxt.Processo.Add(processo);
                }
                NewProcesso = false;
            }

            return rpiTxt;
        }

        private static RpiImported ReadOldTxtFile(string path, Match rpiMatch)
        {
            ProcessoImported processo;
            var rpi = BuildRpi(rpiMatch, out processo);
            RpiNegocio.CurrentRpi = rpi;

            var lines = File.ReadAllLines(path, Encoding.UTF7);

            foreach (var line in lines)
            {
                var lineImport = factory.SeekPartFromAttribute<ImportTxtAttribute>(x => line.StartsWith(x.LineStart));
                lineImport.ValidateLine(line, ref processo);
                
                // TO DO thing one way to catch the Despacho from Old Txt
                //importTxtContext = new ImportTxtContext(new OldLineDespacho());
                
                if (NewProcesso)
                {
                    rpi.Processo.Add(processo);
                }
                NewProcesso = false;
            }

            return rpi;
        }

        private static Match RpiMatch(string path)
        {
            var rpiMatch = RegularExpressions.RpiRegex.Match(path);
            if (!rpiMatch.Success && rpiMatch.Length < 5)
                throw new Exception(string.Format("Nome do arquivo '{0}' está inválido!", path));
            return rpiMatch;
        }

        private static RpiImported BuildRpi(Match rpiMatch, out ProcessoImported processo)
        {
            var rpi = new RpiImported
            {
                NumeroRpi = Convert.ToInt32(rpiMatch.Groups[5].Value),
                DataRpi = rpiMatch.Groups[1].Value,
                Processo = new List<ProcessoImported>()
            };
            processo = new ProcessoImported();
            return rpi;
        }

        #endregion Private Methods
    }
}