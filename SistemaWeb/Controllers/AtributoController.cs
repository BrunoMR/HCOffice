namespace SistemaWeb.Controllers
{
    using System;
    using System.Web.Mvc;
    using BusinessLayer;
    using DTOLayer;
    using Toastr;

    [RoutePrefix("Atributo")]
    public class AtributoController : Controller
    {
        readonly IAtributoNegocio _atributoNegocio = new AtributoNegocio();

        // GET: Natureza
        public ActionResult Index()
        {    
            var atributoList = _atributoNegocio.GetAll();
            return View(atributoList);
        }

        [Route("Detalhe/{id:int}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var atributo = _atributoNegocio.FindById(id);
                return View("Edit", atributo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(Atributo atributo)
        {
            try
            {
                var result = _atributoNegocio.Save(atributo);

                this.AddToastMessage("Atributo", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Atributo", "Erro ao salvar registro!", ToastType.Error);
                throw new Exception(ex.Message);
            }

        }

        // GET: Atributo/Create
        [Route("Novo")]
        public ActionResult Create()
        {
            return View("Edit");
        }

    }
}
