namespace DataLayer
{
    using System;
    using System.Threading.Tasks;
    using DTOLayer;
    using ServiceStack.OrmLite;
    using System.Collections.Generic;

    public class ProcessoCfe4Repository : ARepository, IProcessoCfe4Repository
    {
        public async Task<List<ProcessoCfe4>> ProcessoCfe4LoadByProcessoIdAsync(int processoId)
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().LoadSelectAsync<ProcessoCfe4>(pc => pc.ProcessoId == processoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Processo CFE4 com ID = '{0}'!", processoId), ex.InnerException);
            }
        }
    }
}
