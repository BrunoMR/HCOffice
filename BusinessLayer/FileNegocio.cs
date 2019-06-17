using System.Linq;
using DTOLayer;

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FileNegocio
    {
        public static List<string> Files;
        public static void CopyFileToFolder()
        {
            Files.ForEach(x =>
            {
                var extension = Path.GetExtension(x);
                var targetFolder = extension != null && extension.ToUpper().Equals(".TXT")
                    ? ConfiguracaoNegocio.FindValueByDescription("IMPORTADOS TXT")
                    : ConfiguracaoNegocio.FindValueByDescription("IMPORTADOS XML");

                if (targetFolder != null)
                {
                    var newFileNameWithTargetFolder = targetFolder
                                                        + Path.GetFileName(x)
                                                        + DateTime.Now.ToString("yyyy-MM-dd")
                                                        + "OK";
                    if (File.Exists(newFileNameWithTargetFolder))
                        File.Delete(newFileNameWithTargetFolder);
                    File.Copy(@x, @newFileNameWithTargetFolder);
                }
            });
        }

        public static void DeleteFiles()
        {
            Files.ForEach(File.Delete);
        }

        public static void CopyFileNclToNewFolder(List<RpiImported> rpiList)
        {
            var path = ConfiguracaoNegocio.FindValueByDescription("BUSCA NCL");
            if (path == null)
                throw new Exception("Caminho NCL não encontrado");

            var targetFolder = ConfiguracaoNegocio.FindValueByDescription("IMPORTADOS NCL");

            rpiList.ForEach(rpi =>
            {
                var nclFile = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly)
                    .FirstOrDefault(x => x.Contains("-" + rpi.NumeroRpi + "."));

                if (nclFile != null)
                    if (targetFolder != null)
                    {
                        var newFileNameWithTargetFolder = targetFolder
                                                            + Path.GetFileName(nclFile)
                                                            + DateTime.Now.ToString("yyyy-MM-dd")
                                                            + "OK";

                        if (File.Exists(newFileNameWithTargetFolder))
                            File.Delete(newFileNameWithTargetFolder);
                        File.Copy(@nclFile, @newFileNameWithTargetFolder);
                    }

            });
        }

        public static void DeleteFilesNcl(List<RpiImported> rpiList)
        {
            var path = ConfiguracaoNegocio.FindValueByDescription("BUSCA NCL");

            rpiList.ForEach(rpi =>
            {
                var nclFile = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly)
                    .FirstOrDefault(x => x.Contains("-" + rpi.NumeroRpi + "."));

                if (nclFile != null) File.Delete(nclFile);
            });

            Files.ForEach(File.Delete);
        }
    }
}