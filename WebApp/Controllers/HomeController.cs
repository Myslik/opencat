namespace OpenCat.Controllers
{
    using OpenCat.Data;
    using OpenCat.Models;
    using System;
    using System.Linq;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Security;

    public class HomeController : Controller
    {
        private Repository<User> Repository { get; set; }

        public HomeController()
        {
            Repository = new Repository<User>();
        }

        public ActionResult Index()
        {
            var user = Repository.Get().Where(u => u.identifier == User.Identity.Name).FirstOrDefault();
            return View(user);
        }

        public ActionResult Drop()
        {
            var context = new DataContext();
            context.Database.Drop();
            return RedirectToAction("Index");
        }
    }
}
