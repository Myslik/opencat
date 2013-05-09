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
    using System.Configuration;

    public class HomeController : Controller
    {
        private UserRepository Repository { get; set; }

        public HomeController()
        {
            Repository = new UserRepository();
        }

        public ActionResult Index()
        {
            var user = Repository.Get().Where(u => u.email == User.Identity.Name).FirstOrDefault();
            return View(user);
        }

        public ActionResult Drop()
        {
            FormsAuthentication.SignOut();
            var dbName = ConfigurationManager.AppSettings["dbName"];
            var server = new MongoClient().GetServer();
            var database = server.GetDatabase(dbName);
            database.Drop();
            DataConfig.Initialize();
            return Redirect("/");
        }
    }
}
