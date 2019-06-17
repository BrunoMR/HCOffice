namespace BusinessLayer
{
    using DTOLayer;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProcessoDespachoNegocio
    {
        Task<List<ProcessoDespacho>> ProcessoDespachoLoadByProcessoIdAsync(int processoId);
    }
}
