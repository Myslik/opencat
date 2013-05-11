using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebApp.Specs.Pages
{
    public class BasePage
    {
        public static IWebDriver WebDriver
        {
            get { return WebController.Instance.Selenium; }
        }

        public static string Site
        {
            get
            {
                return string.Format("http://{0}:{1}",
                    ConfigurationManager.AppSettings["target"],
                    ConfigurationManager.AppSettings["port"]);
            }
        }
    }
}
