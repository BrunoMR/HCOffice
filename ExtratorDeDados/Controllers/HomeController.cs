namespace ExtratorDeDados.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using BusinessLayer;

    using Importer;
    using Models;
    using Toastr;

    public class HomeController : Controller
    {
        /// <summary>
        /// The path.
        /// </summary>
        private static string path;

        /// <summary>
        /// The path error.
        /// </summary>
        private static string pathError;

        private IImportFile _importFile;

        public HomeController(IImportFile importFile)
        {
            _importFile = importFile;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            ConfiguracaoNegocio.FindAllClasses();
            path = ConfiguracaoNegocio.FindValueByDescription("ARQUIVOS A IMPORTAR");
            pathError = ConfiguracaoNegocio.FindValueByDescription("ARQUIVO ERRO");


            ViewBag.path = path;
            ViewBag.pathError = pathError;
            return View();
        }

        /// <summary>
        /// The import files.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult ImportFiles(FormCollection collection)
        {
            try
            {
                var response = _importFile.Import(path);
                //var response = ImportFile.Import(path);

                if (response.FirstOrDefault(x => x.Key.Equals("Importação")).Value)
                    this.AddToastMessage("Importação", "Arquivo importado com sucesso!", ToastType.Success);
                else
                    this.AddToastMessage("Importação", "Não foi possível importar o arquivo!", ToastType.Error);

                //if (response.FirstOrDefault(x => x.Key.Equals("Elastic")).Value)
                //    this.AddToastMessage("Sincronização", "Arquivo sincronizado com sucesso!", ToastType.Success);
                //else
                //    this.AddToastMessage("Sincronização", "Não foi possível sincronizar o arquivo!", ToastType.Error);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if ((LogProcess.LastProcesso != null) || (LogProcess.CurrentProcesso != null))
                    message += $" Último Processo validado '{LogProcess.LastProcesso?.NumeroProcesso ?? LogProcess.CurrentProcesso.NumeroProcesso}'! ";

                this.AddToastMessage("Importação", message, ToastType.Error);

                ViewBag.path = path;
                ViewBag.pathError = pathError;
                return View("Index");
            }
            finally
            {
                ViewBag.path = path;
                ViewBag.pathError = pathError;
            }
            return View("Index");
        }

        public ActionResult CreateBackupDatabase()
        {
            try
            {
                if (DataBaseNegocio.CreateBackupDataBase())
                    this.AddToastMessage("Banco de Dados", "Backup criado com sucesso!", ToastType.Success);
                else
                    this.AddToastMessage("Banco de Dados", "Não foi possível criar o Backup!", ToastType.Error);
            }
            catch (Exception)
            {
                ViewBag.path = path;
                ViewBag.pathError = pathError;
                return View("Index");
            }
            finally
            {
                ViewBag.path = path;
                ViewBag.pathError = pathError;
            }
            return View("Index");
        }

        public ActionResult Sync()
        {
            ISyncElastic syncElastic = new SyncElastic();
            //syncElastic.SynchronizationOfAllDatabase();
            syncElastic.SynchronizationProcessesByAllRpis();

            return RedirectToAction("Index");
        }

        public ActionResult SyncData()
        {
            ISyncElastic syncElastic = new SyncElastic();
            syncElastic.SynchronizationOfAllClasses();
            syncElastic.SynchronizationOfAllCfe4S();
            syncElastic.SynchronizationOfAllDespachos();
            syncElastic.SynchronizationOfAllProcuradores();
            syncElastic.SynchronizationOfAllTitulares();
            syncElastic.SynchronizationOfAllClasseAfinidades();
            syncElastic.SynchronizationOfAllPresentations();
            syncElastic.SynchronizationOfAllCountries();
            syncElastic.SynchronizationOfAllStates();
            syncElastic.SynchronizationOfAllAtrhibutes();

            return RedirectToAction("Index");
        }

        public ActionResult SyncByRpi(RpiFilterModel rpi)
        {
            if (ModelState.IsValid)
            {
                ISyncElastic syncElastic = new SyncElastic();
                try
                {
                    var sync = syncElastic.SynchronizationProcessesByRpi(rpi.Start, rpi.End);
                    if (sync)
                        this.AddToastMessage("Sincronização por RPI", "RPI sincronizada(s) com sucesso!", ToastType.Success);
                    else
                        this.AddToastMessage("Sincronização por RPI", "Não foi possível sincronizar a(s) RPI!", ToastType.Error);
                }
                catch (Exception ex)
                {
                    this.AddToastMessage("Sincronização por RPI", ex.Message, ToastType.Error);
                }

            }

            return RedirectToAction("Index");
        }

    }
}
