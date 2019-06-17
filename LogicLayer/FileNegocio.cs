using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ExtratorDeDados.Negocio
{
    public class FileNegocio
    {
        public static List<string> Files;
        public static void CopyFileToFolder()
        {
            Files.ForEach(x =>
            {
                var extension = Path.GetExtension(x);
                var targetFolder = extension != null && extension.ToUpper().Equals(".TXT")
                    ? ConfiguracaoNegocio.FindValueByDescription("IMPORTARDOS TXT")
                    : ConfiguracaoNegocio.FindValueByDescription("IMPORTARDOS XML");

                if (targetFolder != null)
                {
                    var newFileNameWithTargetFolder = targetFolder 
                                                        + Path.GetFileName(x)
                                                        + DateTime.Now.ToString("yyyy-MM-dd")
                                                        +"OK";
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
    }
}