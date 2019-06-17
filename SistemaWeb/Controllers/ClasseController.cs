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
    [RoutePrefix("Classe")]
    public class ClasseController : Controller
    {
        readonly IClasseNegocio _classeNegocio = new ClasseNegocio();

        // GET: Classe
        public ActionResult Index()
        {
            var listClasse = _classeNegocio.GetAll();
            return View(listClasse.Select(x => new DTOLayer.Classe { NumeroClasse = x.NumeroClasse.Trim(), Descricao = x.Descricao }).ToList());
        }

        [Route("Detalhe/{code}")]
        public ActionResult Edit(string code)
        {
            try
            {
                var classe = _classeNegocio.FindByCode(code);
                return View("Edit", classe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(Classe classe)
        {
            try
            {
                var result = _classeNegocio.Save(classe);

                this.AddToastMessage("Classe", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Classe", "Erro ao salvar registro!", ToastType.Error);
                throw new Exception(ex.Message);
            }

        }

        [Route("Novo")]
        public ActionResult Create()
        {
            var classe = new Classe() {IsNew = true};
            return View("Edit", classe);
        }

    }
}
