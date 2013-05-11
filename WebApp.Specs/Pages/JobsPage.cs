using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebApp.Specs.Pages
{
    public class JobsPage : MainPage
    {
        public class JobRow
        {
            public JobRow(IWebElement element)
            {
                Element = element;
            }

            public IWebElement Element { get; private set; }

            public IWebElement Name { get { return Element.FindElement(By.ClassName("job-name")); } }
            public IWebElement Description { get { return Element.FindElement(By.ClassName("job-description")); } }
            public IWebElement Words { get { return Element.FindElement(By.ClassName("job-words")); } }

            public IWebElement Remove { get { return Element.FindElement(By.ClassName("job-remove")); } }
        }

        public IWebElement New
        {
            get { return WebDriver.FindElement(By.Id("job-new")); }
        }

        public IWebElement Reload
        {
            get { return WebDriver.FindElement(By.Id("jobs-reload")); }
        }

        public IEnumerable<JobRow> Jobs
        {
            get
            {
                return WebDriver.FindElements(By.ClassName("job-row")).Select(r => new JobRow(r));
            }
        }
    }
}
