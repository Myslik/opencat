namespace OpenCat.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
