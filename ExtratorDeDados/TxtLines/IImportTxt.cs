using DTOLayer;

namespace ExtratorDeDados.TxtLines
{
    public interface IImportTxt
    {
        void ValidateLine(string line, ref ProcessoImported processo);
    }
}
