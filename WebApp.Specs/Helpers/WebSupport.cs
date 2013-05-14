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
            var database = BusinessHelper.Database;
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
