namespace SistemaWeb.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using BusinessLayer;
    using DTOLayer;
    using Toastr;

    [RoutePrefix("Estado")]
    public class EstadoController : Controller
    {
        readonly IEstadoNegocio _estadoNegocio = new EstadoNegocio();

        // GET: Pais
        public ActionResult Index()
        {
            var estadoList = _estadoNegocio.GetAllAsync();
            return View(estadoList);
        }

        [Route("Detalhe/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var estado = await _estadoNegocio.FindByIdAsync(id);
                return View("Edit", estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(Uf uf)
        {
            try
            {
                _estadoNegocio.Save(uf);

                this.AddToastMessage("Estado", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Estado", "Erro ao salvar registro!", ToastType.Error);
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
