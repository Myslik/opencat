using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using WebApp.Specs.Pages;

namespace WebApp.Specs
{
    [Binding]
    public class NavigationSteps : WebStepsBase
    {
        [Then(@"I can visit all sections"), Scope(Tag = "web")]
        public void ThenICanVisitAllSections()
        {
            On<MainPage>(page =>
            {
                foreach (var section in page.Sections)
                {
                    section.Click();
                    Assert.IsTrue(section.IsActive());
                }
            });
        }
    }
}
