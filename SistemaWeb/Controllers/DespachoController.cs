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
    [RoutePrefix("Despacho")]
    public class DespachoController : Controller
    {
        readonly IDespachoNegocio _despachoNegocio = new DespachoNegocio();

        // GET: Despacho
        public ActionResult Index()
        {
            var listDespacho = _despachoNegocio.GetAll();
            return View(listDespacho);
        }

        [Route("Detalhe/{id:int}")]
        public ActionResult Edit(int id)
        {
            var despacho = _despachoNegocio.FindById(id);

            BuildDropDownListTipoSituacao(despacho);
            BuildDropDownListTipoDespacho(despacho);

            return View("Edit", despacho);
        }

        [HttpPost]
        public ActionResult Save(Despacho despacho)
        {
            try
            {
                var result = _despachoNegocio.Save(despacho);

                this.AddToastMessage("Despacho", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Despacho", "Erro ao salvar registro!", ToastType.Error);
                throw new Exception(ex.Message);
            }

        }

        [Route("Novo")]
        public ActionResult Create()
        {
            BuildDropDownListTipoSituacao(null);
            BuildDropDownListTipoDespacho(null);

            return View("Edit");
        }

        private void BuildDropDownListTipoSituacao(Despacho despacho)
        {
            ITipoSituacaoNegocio tipoSituacaoNegocio = new TipoSituacaoNegocio();

            ViewBag.TipoSituacao = new SelectList(
                tipoSituacaoNegocio.GetAll(),
                "Tipo",
                "Descricao",
                despacho?.Situacao
                );
        }

        private void BuildDropDownListTipoDespacho(Despacho despacho)
        {
            ITipoDespachoNegocio tipoDespachoNegocio = new TipoDespachoNegocio();

            ViewBag.TipoDespacho = new SelectList(
                tipoDespachoNegocio.GetAll(),
                "Tipo",
                "Descricao",
                despacho?.Tipo
                );
        }

    }
}
