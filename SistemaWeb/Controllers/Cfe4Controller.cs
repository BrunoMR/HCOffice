namespace SistemaWeb.Controllers
{
    using System;
    using System.Web.Mvc;
    using BusinessLayer;
    using DTOLayer;
    using PagedList;
    using Toastr;

    [RoutePrefix("Cfe4")]
    public class Cfe4Controller : Controller
    {
        /// <summary>
        /// The _cfe 4 negocio.
        /// </summary>
        private readonly ICfe4Negocio _cfe4Negocio;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cfe4Controller"/> class.
        /// </summary>
        /// <param name="cfe4Negocio">
        /// The cfe 4 negocio.
        /// </param>
        public Cfe4Controller(ICfe4Negocio cfe4Negocio)
        {
            _cfe4Negocio = cfe4Negocio;
        }

        // GET: Cfe4
        public ActionResult Index(int? page)
        {
            var pageNumber = (page ?? 1);
            var pageSize = 50;
            int count;
            var listCfe4 = _cfe4Negocio.GetAll(pageNumber, pageSize, out count); // .GetAll(pageNumber, pageSize, out count);
            var listCfe4Paginated = new StaticPagedList<CFE4>(listCfe4, pageNumber, pageSize, count);

            return View(listCfe4Paginated);
        }

        [Route("Detalhe/{id:int}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var cfe4 = _cfe4Negocio.FindById(id);
                return View("Edit", cfe4);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(CFE4 cfe4)
        {
            try
            {
                var result = _cfe4Negocio.AddOrUpdate(cfe4);

                this.AddToastMessage("CFE(4)", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("CFE(4)", "Erro ao salvar registro!", ToastType.Error);
                throw new Exception(ex.Message);
            }

        }

        [Route("Novo")]
        public ActionResult Create()
        {
            return View("Edit");
        }
    }
}
