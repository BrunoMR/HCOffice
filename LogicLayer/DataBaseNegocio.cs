using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtratorDeDados.Repository;

namespace ExtratorDeDados.Negocio
{
    public class DataBaseNegocio
    {
        public static bool CreateBackupDataBase()
        {
            try
            {
                ConfiguracaoNegocio.FindAllClasses();

                var path = ConfiguracaoNegocio.FindValueByDescription("BACKUPHCOFFICE");
                path += "HCOFFICE_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".bak";

                var dataBaseRepository = new DataBaseRepository();
                return dataBaseRepository.CreateBackupDatabase(path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
