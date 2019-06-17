namespace BusinessLayer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTOLayer;

    public interface IProcessoNegocio
    {
        #region CRUD

        List<Processo> GetAll(int pageNumber, int pageSize, out int count);
        Task<Processo> FindById(int id);
        Processo AddOrUpdate(Processo processo);

        #endregion CRUD

        Task<List<Processo>> GetAllFullProcessoAsync();

        Task<Processo> GetAllOfProcessoByNumeroAsync(string numero);

        Task<Processo> FindByNumeroProcesso(string numero);
    }
}
