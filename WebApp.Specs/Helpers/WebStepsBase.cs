using System.Configuration;
using OpenQA.Selenium;

namespace WebApp.Specs
{
    public abstract class WebStepsBase
    {
        protected IWebDriver WebDriver
        {
            get { return WebController.Instance.Selenium; }
        }

        protected string Site
        {
            get { 
                return string.Format("http://{0}:{1}", 
                    ConfigurationManager.AppSettings["target"], 
                    ConfigurationManager.AppSettings["port"]); 
            }
        }
    }
}
