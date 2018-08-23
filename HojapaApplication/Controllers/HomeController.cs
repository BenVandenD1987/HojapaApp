using System.Web.Mvc;

namespace HojapaApplication.Controllers {


    public class HomeController : Controller {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About() {
            ViewBag.Message = "Ben Vanden Dries";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Mobyus";

            return View();
        }
    }
}
