using System.Web.Mvc;
using ProductManager.Web.Filters;

namespace ProductManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AdministratorFilter]
        public ActionResult Configuration()
        {
            return View();
        }


    }
}