namespace BusinessLayer
{
    using DTOLayer;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProcessoCfe4Negocio
    {
        Task<List<ProcessoCfe4>> ProcessoCfe4LoadByProcessoIdAsync(int processoId);
    }
}
