using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebApp.Specs.Pages
{
    public class EditJobPage : MainPage
    {
        public class AttachmentRow
        {
            public AttachmentRow(IWebElement element)
            {
                Element = element;
            }

            public IWebElement Element { get; private set; }

            public IWebElement Name { get { return Element.FindElement(By.ClassName("attachment-name")); } }
            public IWebElement Remove { get { return Element.FindElement(By.ClassName("attachment-remove")); } }
        }

        public IWebElement Name
        {
            get { return WebDriver.FindElement(By.Id("name")); }
        }

        public IWebElement Description
        {
            get { return WebDriver.FindElement(By.Id("description")); }
        }

        public IWebElement Upload
        {
            get { return WebDriver.FindElement(By.Id("job-upload")).FindElement(By.TagName("input")); }
        }

        public IEnumerable<AttachmentRow> Attachments
        {
            get
            {
                return WebDriver.FindElements(By.ClassName("attachment-row")).Select(r => new AttachmentRow(r));
            }
        }
    }
}
