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
    [RoutePrefix("Apresentacao")]
    public class ApresentacaoController : Controller
    {
        readonly ITipoApresentacaoNegocio _tipoApresentacaoNegocio = new TipoApresentacaoNegocio();

        // GET: Apresentacao
        public ActionResult Index()
        {
            var listTipoApresentacao = _tipoApresentacaoNegocio.GetAll();
            return View(listTipoApresentacao);
        }

        [Route("Detalhe/{id:int}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var tipoApresentacao = _tipoApresentacaoNegocio.FindById(id);
                return View("Edit", tipoApresentacao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        [HttpPost]
        public ActionResult Save(TipoApresentacao tipoApresentacao)
        {
            try
            {
                var result = _tipoApresentacaoNegocio.Save(tipoApresentacao);

                this.AddToastMessage("Apresentação", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Apresentação", "Erro ao salvar registro!", ToastType.Error);
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
