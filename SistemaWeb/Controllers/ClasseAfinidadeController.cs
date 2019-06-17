namespace SistemaWeb.Controllers
{
    using System.Web.Mvc;
    using BusinessLayer;

    public class ClasseAfinidadeController : Controller
    {
        // GET: ClasseAfinidade
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 50;

            IClasseAfinidadeNegocio classeAfinidadeNegocio = new ClasseAfinidadeNegocio();
            var listClasseAfinidade = classeAfinidadeNegocio.GetAll(pageNumber, pageSize);

            return View(listClasseAfinidade);
        }
    }
}
