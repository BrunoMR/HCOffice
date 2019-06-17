using System.Threading.Tasks;

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;

    using DataLayer;
    using DTOLayer;

    public class ProcessoCfe4Negocio : IProcessoCfe4Negocio
    {
        readonly IProcessoCfe4Repository _processoCfe4Repository = new ProcessoCfe4Repository();

        public async Task<List<ProcessoCfe4>> ProcessoCfe4LoadByProcessoIdAsync(int processoId)
        {
            try
            {
                return await _processoCfe4Repository.ProcessoCfe4LoadByProcessoIdAsync(processoId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        
    }
}
