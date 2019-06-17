using System;

namespace DataLayer
{
    public interface IDataBaseRepository
    {
        bool CreateBackupDatabase(string path);
    }
}
