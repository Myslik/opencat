using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace WebApp.Specs
{
    [Binding]
    public class AuthenticationSteps : WebStepsBase
    {
        [StepDefinition(@"I authenticate with valid credentials")]
        public void WhenIAuthenticateWithValidCredentials()
        {
            WebDriver.Navigate().GoToUrl(Site);
            WebDriver.FindElement(By.Name("email")).SendKeys("user@gmail.com");
            WebDriver.FindElement(By.Name("password")).SendKeys("correct");
            WebDriver.FindElement(By.ClassName("form-signin")).Submit();
        }
        
        [When(@"I authenticate with invalid credentials")]
        public void WhenIAuthenticateWithInvalidCredentials()
        {
            WebDriver.Navigate().GoToUrl(Site);
            WebDriver.FindElement(By.Name("email")).SendKeys("unknown@gmail.com");
            WebDriver.FindElement(By.Name("password")).SendKeys("badpass");
            WebDriver.FindElement(By.ClassName("form-signin")).Submit();
        }
        
        [Then(@"I should be redirected to application")]
        public void ThenIShouldBeRedirectedToApplication()
        {
            Assert.AreEqual("OpenCAT", WebDriver.FindElement(By.TagName("h1")).Text);
        }
        
        [Then(@"I should see invalid credentials message")]
        public void ThenIShouldSeeInvalidCredentialsMessage()
        {
            Assert.AreEqual("Authentication failed", WebDriver.FindElement(By.ClassName("alert-error")).Text);
        }

        [When(@"I log out")]
        public void WhenILogOut()
        {
            WebDriver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
