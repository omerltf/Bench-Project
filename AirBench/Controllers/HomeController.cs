using System.Web.Mvc;

namespace AirBench.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Air Bench";

            return View();
        }
    }
}
