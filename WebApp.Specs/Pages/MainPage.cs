using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebApp.Specs.Pages
{
    public class MainPage : BasePage
    {
        public MainPage()
        {
            Sections = section_names.Select(n => WebDriver.FindElement(By.LinkText(n)));
        }

        public IWebElement Logout
        {
            get { return WebDriver.FindElement(By.LinkText("Logout")); }
        }

        private string[] section_names = new string[] { "Jobs", "API" };
        public IEnumerable<IWebElement> Sections { get; private set; }

        public void Visit(string section)
        {
            Debug.Assert(section_names.Contains(section), "Given section is not defined");
            WebDriver.FindElement(By.LinkText(section)).Click();
        }
    }
}
