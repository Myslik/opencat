using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace WebApp.Specs
{
    public class WebController
    {
        public static WebController Instance = new WebController();

        public IWebDriver Selenium { get; private set; }

        private void Trace(string message)
        {
            Console.WriteLine("-> {0}", message);
        }

        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        public void Start()
        {
            if (Selenium != null)
                return;

            Selenium = new PhantomJSDriver();
            Selenium.Manage().Timeouts().ImplicitlyWait(DefaultTimeout);

            Trace("PhantomJS ready");
        }

        public void Stop()
        {
            if (Selenium == null)
                return;

            try
            {
                Selenium.Quit();
                Selenium.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Selenium stop error");
            }
            Selenium = null;
            Trace("PhantomJS down");
        }
    }
}
