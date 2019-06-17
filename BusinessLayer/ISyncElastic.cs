namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ISyncElastic
    {
        /// <summary>
        /// The get count to sync.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int GetCountToSync();

        /// <summary>
        /// The synchronization of all database.
        /// </summary>
        void SynchronizationOfAllDatabase();

        /// <summary>
        /// The synchronization of imported processes.
        /// </summary>
        /// <param name="rpiImporteds">
        /// The rpi importeds.
        /// </param>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfImportedProcesses(IEnumerable<RpiImported> rpiImporteds);

        /// <summary>
        /// The synchronization processes by all rpis.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationProcessesByAllRpis();

        /// <summary>
        /// The synchronization processes by rpi.
        /// </summary>
        /// <param name="startRpi">
        /// The start rpi.
        /// </param>
        /// <param name="endRpi">
        /// The end rpi.
        /// </param>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationProcessesByRpi(int startRpi, int endRpi);

        /// <summary>
        /// The synchronization of all procuradores.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllProcuradores();

        /// <summary>
        /// The synchronization of all titulares.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllTitulares();

        /// <summary>
        /// The synchronization of all classes.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllClasses();

        /// <summary>
        /// The synchronization of all cfe 4 s.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllCfe4S();

        /// <summary>
        /// The synchronization of all despachos.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllDespachos();

        /// <summary>
        /// The synchronization of all classe afinidades.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllClasseAfinidades();

        /// <summary>
        /// The synchronization of all presentations.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllPresentations();

        /// <summary>
        /// The synchronization of all countries.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllCountries();

        /// <summary>
        /// The synchronization of all states.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllStates();

        /// <summary>
        /// The synchronization of all atrhibutes.
        /// </summary>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfAllAtrhibutes();

        /// <summary>
        /// The synchronization of Rpi
        /// </summary>
        /// <param name="numberRpi">
        /// The number Rpi.
        /// </param>
        /// <returns>
        /// Returns if synchronization worked
        /// </returns>
        bool SynchronizationOfRpi(int numberRpi);
    }
}
