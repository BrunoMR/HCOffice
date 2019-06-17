namespace DataLayer
{
    using System.Collections.Generic;
    using System.Data;
    using DTOLayer;
    using DTOLayer.Indexes;

    public interface ISyncElasticRepository
    {
        List<ProcessoSync> GetInRange(int start, int end);

        /// <summary>
        /// Get the total rows that will be import
        /// </summary>
        /// <returns>Returns the count of rows imported</returns>
        int GetCountToSync();

        /// <summary>
        /// The get all ids of processo.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ProcessoIds> GetAllIdsOfProcesso();

        /// <summary>
        /// The get full processos.
        /// </summary>
        /// <param name="modelDataTable">
        /// The model data table.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<ProcessoSync> GetFullProcessos(DataTable modelDataTable);

        /// <summary>
        /// The get full processos by rpi.
        /// </summary>
        /// <param name="startRpi">
        /// The start rpi.
        /// </param>
        /// <param name="endRpi">
        /// The end rpi.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<ProcessoSync> GetFullProcessosByRpi(int startRpi, int endRpi);

        /// <summary>
        /// Return the processes that recently imported
        /// </summary>
        /// <param name="modelDataTable"></param>
        /// <returns></returns>
        IEnumerable<ProcessoSync> GetImportedProcessos(DataTable modelDataTable);

        /// <summary>
        /// Return all procurator in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProcuradorIndex> GetAllProcuradores();

        /// <summary>
        /// Return all owners in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<TitularIndex> GetAllTitulares();

        /// <summary>
        /// Return all classes in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<ClasseIndex> GetAllClasses();

        /// <summary>
        /// Return all cfe4s in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Cfe4Index> GetAllCfe4S();

        /// <summary>
        /// Return all despachos in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<DespachoIndex> GetAllDespachos();

        /// <summary>
        /// Return all class similarities in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<ClasseAfinidadeSync> GetAllClassSimilarities();

        /// <summary>
        /// Return all presentations in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApresentacaoIndex> GetAllPresentations();

        /// <summary>
        /// Return all coutries in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<PaisIndex> GetAllCountries();

        /// <summary>
        /// Return all states in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<EstadoIndex> GetAllStates();

        /// <summary>
        /// Return all athributes in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<AtributoIndex> GetAllAthributes();
    }
}
