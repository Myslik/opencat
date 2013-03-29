namespace OpenCat.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Security;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Drop()
        {
            var context = new DataContext();
            context.Database.Drop();
            return RedirectToAction("Index");
        }
    }
}
