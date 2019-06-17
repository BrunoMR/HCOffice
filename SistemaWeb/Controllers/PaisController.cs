namespace SistemaWeb.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using BusinessLayer;
    using DTOLayer;
    using Toastr;

    [Authorize(Roles = "Root, Admin")]
    [RoutePrefix("Pais")]
    public class PaisController : Controller
    {
        readonly IPaisNegocio _paisNegocio = new PaisNegocio();

        // GET: Pais
        public ActionResult Index()
        {
            var listPais = _paisNegocio.GetAllAsync();
            return View(listPais);
        }

        [Route("Detalhe/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var pais = await _paisNegocio.FindByIdAsync(id);
                return View("Edit", pais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(Pais pais)
        {
            try
            {
                _paisNegocio.AddOrUpdate(pais);

                this.AddToastMessage("País", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("País", "Erro ao salvar registro!", ToastType.Error);
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
