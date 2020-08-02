using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DTOLayer;
using BusinessLayer;
using Utils;

namespace ExtratorDeDados.Importer
{
    public class ImportFile : IImportFile
    {
        private IRpiNegocio _rpiNegocio;

        public ImportFile(IRpiNegocio rpiNegocio)
        {
            _rpiNegocio = rpiNegocio;
        }

        public Dictionary<string, bool> Import(string path)
        {
            var rpis = GetAllFiles(path);

            //var process = new RpiNegocio();
            var validations = _rpiNegocio.ProcessRpi(rpis);

            var result = new Dictionary< string, bool>();

            GC.Collect();
            if (validations.TrueForAll(x => x.IsValid))
            {
                result.Add("Importação", true);

                //ISyncElastic syncElastic = new SyncElastic();
                //result.Add("Elastic", syncElastic.SynchronizationProcessesByAllRpis()); // .SynchronizationOfImportedProcesses(rpis));
                GC.Collect();

                FileNegocio.CopyFileToFolder();
                FileNegocio.DeleteFiles();

                var justNumeroRpi = rpis.Select(rpi => new RpiImported() { NumeroRpi = rpi.NumeroRpi }).ToList();
                FileNegocio.CopyFileNclToNewFolder(justNumeroRpi);
                FileNegocio.DeleteFilesNcl(justNumeroRpi);

                return result;
            }

            result.Add("Importação", false);
            return result;
        }

        private List<RpiImported> GetAllFiles(string path)
        {
            FileNegocio.Files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => RegularExpressions.FilesExtensions.IsMatch(file))
                .OrderBy(file => file).ToList();

            return FileNegocio.Files.Select(ImporAnytFile).ToList();
        }

        private RpiImported ImporAnytFile(string path)
        {
            return path.ToUpper().EndsWith(".TXT") ?
                ImportTxt.TxtImport(path) :
                ImportXml.Deserializar_Processo(path);
        }
    }
}