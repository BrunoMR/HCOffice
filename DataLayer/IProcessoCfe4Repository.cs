namespace DataLayer
{
    using System.Threading.Tasks;
    using DTOLayer;
    using System.Collections.Generic;

    public interface IProcessoCfe4Repository
    {
        Task<List<ProcessoCfe4>> ProcessoCfe4LoadByProcessoIdAsync(int processoId);
    }
}
