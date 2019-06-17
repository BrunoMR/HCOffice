using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DTOLayer;
using SistemaWeb.Toastr;

namespace SistemaWeb.Controllers
{
    [RoutePrefix("Natureza")]
    public class NaturezaController : Controller
    {
        readonly ITipoNaturezaNegocio _tipoNaturezaNegocio = new TipoNaturezaNegocio();

        // GET: Natureza
        public ActionResult Index()
        {    
            var listTipoNatureza = _tipoNaturezaNegocio.GetAll();
            return View(listTipoNatureza);
        }

        [Route("Detalhe/{id:int}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var tipoNatureza = _tipoNaturezaNegocio.FindById(id);
                return View("Edit", tipoNatureza);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(TipoNatureza tipoNatureza)
        {
            try
            {
                var result = _tipoNaturezaNegocio.Save(tipoNatureza);

                this.AddToastMessage("Natureza", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Natureza", "Erro ao salvar registro!", ToastType.Error);
                throw new Exception(ex.Message);
            }

        }

        // GET: Apresentacao/Create
        [Route("Novo")]
        public ActionResult Create()
        {
            return View("Edit");
        }

    }
}
