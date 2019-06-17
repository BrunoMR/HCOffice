namespace SistemaWeb.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (User.IsInRole("Operator"))
            {
                return RedirectToAction("Index", "Search");
            }

            return View();
        }
    }
}