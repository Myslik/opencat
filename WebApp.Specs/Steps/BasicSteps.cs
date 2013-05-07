using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace WebApp.Specs
{
    [Binding]
    public class BasicSteps : WebStepsBase
    {
        [Given(@"I open the landing page")]
        public void GivenIOpenTheLandingPage()
        {
            WebDriver.Navigate().GoToUrl(Site);
        }
        
        [Then(@"I should see the login form")]
        public void ThenIShouldSeeTheLoginForm()
        {
            Assert.AreEqual("OpenCAT", WebDriver.Title);
            Assert.AreEqual("Please sign in", WebDriver.FindElement(By.ClassName("form-signin-heading")).Text);
        }
    }
}
