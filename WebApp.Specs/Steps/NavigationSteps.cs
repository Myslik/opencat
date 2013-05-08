using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace WebApp.Specs
{
    [Binding]
    public class NavigationSteps : WebStepsBase
    {        
        [Then(@"I can visit all sections")]
        public void ThenICanVisitAllSections()
        {
            var sections = new string[] { "Jobs", "API" };
            foreach (var section in sections)
            {
                var link = WebDriver.FindElement(By.LinkText(section));
                link.Click();
                Assert.IsTrue(link.GetAttribute("class").Contains("active"));
            }
        }
    }
}
