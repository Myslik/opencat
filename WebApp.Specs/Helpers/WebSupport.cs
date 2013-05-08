using TechTalk.SpecFlow;

namespace WebApp.Specs
{
    [Binding]
    public static class WebSupport
    {
        [BeforeScenario]
        public static void BeforeWebScenario()
        {
            WebController.Instance.Start();
        }

        [AfterScenario]
        public static void AfterWebScenario()
        {
            WebController.Instance.Selenium.Manage().Cookies.DeleteAllCookies();
        }

        [AfterTestRun]
        public static void AfterWebFeature()
        {
            WebController.Instance.Stop();
        }
    }
}
