using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebApp.Specs.Pages
{
    public class NewJobPage : MainPage
    {
        public IWebElement Name
        {
            get { return WebDriver.FindElement(By.Id("name")); }
        }

        public IWebElement Description
        {
            get { return WebDriver.FindElement(By.Id("description")); }
        }

        public IWebElement Words
        {
            get { return WebDriver.FindElement(By.Id("words")); }
        }

        public IWebElement Save
        {
            get { return WebDriver.FindElement(By.Id("save")); }
        }

        public IWebElement Cancel
        {
            get { return WebDriver.FindElement(By.Id("cancel")); }
        }
    }
}
