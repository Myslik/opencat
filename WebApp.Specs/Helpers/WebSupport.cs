using System.Configuration;
using MongoDB.Driver;
using OpenCat;
using TechTalk.SpecFlow;

namespace WebApp.Specs
{
    [Binding]
    public static class WebSupport
    {
        // Clean database before every Feature so Background can be used to fill the application with relevant test data
        [BeforeFeature]
        public static void BeforeWebFeature()
        {
            var dbName = ConfigurationManager.AppSettings["dbName"];
            var server = new MongoClient().GetServer();
            var database = server.GetDatabase(dbName);
            database.Drop();
            DataConfig.Initialize();
        }

        [BeforeScenario, Scope(Tag = "WebUI")]
        public static void BeforeWebScenario()
        {
            WebController.Instance.Start();
            WebController.Instance.Selenium.Manage().Cookies.DeleteAllCookies();
        }
        
        [AfterTestRun]
        public static void AfterTestRun()
        {
            WebController.Instance.Stop();
        }
    }
}
