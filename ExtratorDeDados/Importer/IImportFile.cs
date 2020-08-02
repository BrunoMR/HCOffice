using System.Collections.Generic;

namespace ExtratorDeDados.Importer
{
    public interface IImportFile
    {
        Dictionary<string, bool> Import(string path);
    }
}