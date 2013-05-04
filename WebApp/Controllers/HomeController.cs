namespace OpenCat.Controllers
{
    using OpenCat.Data;
    using OpenCat.Models;
    using System;
    using System.Linq;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Security;
    using MongoDB.Driver;

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
            var server = new MongoClient().GetServer();
            var database = server.GetDatabase("OpenCAT");
            database.Drop();
            return RedirectToAction("Index");
        }
    }
}
