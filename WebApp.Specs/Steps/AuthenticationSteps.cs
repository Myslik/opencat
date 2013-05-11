using System;
using System.Linq;
using System.Collections;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using WebApp.Specs.Pages;

namespace WebApp.Specs
{
    [Binding]
    public class AuthenticationSteps : WebStepsBase
    {
        [Given(@"I open the landing page"), Scope(Tag = "web")]
        public void GivenIOpenTheLandingPage()
        {
            GoHome();
        }

        [Then(@"I should see the login form"), Scope(Tag = "web")]
        public void ThenIShouldSeeTheLoginForm()
        {
            Assert.AreEqual("OpenCAT", WebDriver.Title);
            Assert.AreEqual("Please sign in", On<LoginPage>().Heading.Text);
        }

        [StepDefinition(@"I authenticate with valid credentials"), Scope(Tag = "web")]
        public void WhenIAuthenticateWithValidCredentials()
        {
            GoHome();
            On<LoginPage>(page =>
            {
                page.Email.SendKeys("user@gmail.com");
                page.Password.SendKeys("correct");
                page.Submit();
            });
        }

        [When(@"I authenticate with invalid credentials"), Scope(Tag = "web")]
        public void WhenIAuthenticateWithInvalidCredentials()
        {
            GoHome();
            On<LoginPage>(page =>
            {
                page.Email.SendKeys("unknown@gmail.com");
                page.Password.SendKeys("badpass");
                page.Submit();
            });
        }

        [Then(@"I should be redirected to application"), Scope(Tag = "web")]
        public void ThenIShouldBeRedirectedToApplication()
        {
            Assert.AreEqual("OpenCAT", WebDriver.FindElement(By.TagName("h1")).Text);
        }

        [Then(@"I should see invalid credentials message"), Scope(Tag = "web")]
        public void ThenIShouldSeeInvalidCredentialsMessage()
        {
            Assert.AreEqual("Authentication failed", On<LoginPage>().Alert.Text);
        }

        [When(@"I log out"), Scope(Tag = "web")]
        public void WhenILogOut()
        {
            On<MainPage>().Logout.Click();
        }
    }
}
