namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Connections;
    using DTOLayer;
    using ServiceStack.OrmLite;
    using System.Linq;

    public class ProcessoDespachoRepository : ARepository, IProcessoDespachoRepository
    {
        public void BulkInsert(DataTable dataTablemodel, SqlTransaction transaction)
        {
            try
            {
                using (var cmd = new SqlCommand("INSERT_PROCESSO_DESPACHO"))
                {
                    var connection = ConnectionDapper.RetornaInstancia();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection.AbreConexao();
                    cmd.Parameters.AddWithValue("@tableProcessoDespachos", dataTablemodel);
                    cmd.Transaction = transaction;
                    cmd.CommandTimeout = 1200;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível inserir os Processo Despachos", ex.InnerException);
            }
        }

        public async Task<List<ProcessoDespacho>> GetAllOfProcesso(ProcessoDespacho processoDespacho)
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().SelectAsync<ProcessoDespacho>(x => x.ProcessoId == processoDespacho.ProcessoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Processo Despacho '{0}' na tabela de PROCESSO_DESPACHO!", processoDespacho.ProcessoId), ex.InnerException);
            }
        }

        public async Task<List<ProcessoDespacho>> ProcessoDespachoLoadByProcessoIdAsync(int processoId)
        {
            try
            {
                return await ConnectionOrmLite.OpenConnection().LoadSelectAsync<ProcessoDespacho>(pd => pd.ProcessoId == processoId);
                
                //var db = ConnectionOrmLite.AbreConexao();

                //var opa = db.LoadSelect<ProcessoDespacho>(
                //    db.From<ProcessoDespacho>()
                //        .Join<Despacho>()
                //        .Join<TipoSituacao, Despacho>((t, d) => t.Tipo == d.Situacao)
                //        .Join<Protocolo>()
                //        .Join<Rpi>()
                //        .Where(pd => pd.Id == id));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível procurar o Processo Despacho '{0}' na tabela de PROCESSO_DESPACHO!", processoId), ex.InnerException);
            }
        }
    }
}
