namespace OpenCat.Controllers
{
    using OpenCat.Models;
    using System;
    using System.Linq;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Security;
    using MongoDB.Driver;
    using System.Configuration;
    using OpenCat.Services;

    public class HomeController : Controller
    {
        private UserService Users { get; set; }

        public HomeController(UserService service)
        {
            Users = service;
        }

        public ActionResult Index()
        {
            var user = Users.Read().Where(u => u.email == User.Identity.Name).FirstOrDefault();
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
