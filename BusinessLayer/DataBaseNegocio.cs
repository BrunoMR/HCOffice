namespace BusinessLayer
{
    using System;
    using DataLayer;

    public class DataBaseNegocio
    {
        public static bool CreateBackupDataBase()
        {
            try
            {
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
