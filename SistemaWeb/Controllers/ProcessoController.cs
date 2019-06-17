namespace SistemaWeb.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using BusinessLayer;
    using DTOLayer;

    using PagedList;

    using Toastr;

    [RoutePrefix("Processo")]
    public class ProcessoController : Controller
    {
        readonly IProcessoNegocio _processoNegocio = new ProcessoNegocio();

        // GET: Processo
        public ActionResult Index(int? page)
        {
            var pageNumber = (page ?? 1);
            var pageSize = 50;

            int count;
            var listProcesso =  _processoNegocio.GetAll(pageNumber, pageSize, out count);

            var listProcessoPaginated = new StaticPagedList<Processo>(listProcesso, pageNumber, pageSize, count);

            return View(listProcessoPaginated);
        }
        
        [Route("Detalhe/{code}")]
        public async Task<ActionResult> Edit(string code)
        {
            try
            {
                var processo = await _processoNegocio.FindByNumeroProcesso(code);

                BuildDropDownListTipoApresentacao(processo);
                BuildDropDownListTipoNatureza(processo);

                return View("Edit", processo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(Processo processo)
        {
            try
            {
                var result = _processoNegocio.AddOrUpdate(processo);

                this.AddToastMessage("Processo", "Registro salvo com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Processo", "Erro ao salvar registro!", ToastType.Error);
                throw new Exception(ex.Message);
            }

        }

        [Route("Novo")]
        public ActionResult Create()
        {
            BuildDropDownListTipoApresentacao(null);
            BuildDropDownListTipoNatureza(null);

            return View("Edit");
        }

        private void BuildDropDownListTipoApresentacao(Processo processo)
        {
            ITipoApresentacaoNegocio tipoApresentacaoNegocio = new TipoApresentacaoNegocio();

            ViewBag.TipoApresentacao = new SelectList(
                tipoApresentacaoNegocio.GetAll(),
                "Id",
                "Descricao",
                processo?.TipoApresentacaoId
                );
        }

        private void BuildDropDownListTipoNatureza(Processo processo)
        {
            ITipoNaturezaNegocio tipoNaturezaNegocio = new TipoNaturezaNegocio();

            ViewBag.TipoNatureza = new SelectList(
                tipoNaturezaNegocio.GetAll(),
                "Id",
                "Descricao",
                processo?.TipoNaturezaId
                );
        }
    }
}
