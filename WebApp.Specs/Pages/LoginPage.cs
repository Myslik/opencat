using OpenQA.Selenium;

namespace WebApp.Specs.Pages
{
    public class LoginPage : BasePage
    {
        public IWebElement Heading
        {
            get { return WebDriver.FindElement(By.ClassName("form-signin-heading")); }
        }

        public IWebElement Email
        {
            get { return WebDriver.FindElement(By.Name("email")); }
        }

        public IWebElement Password
        {
            get { return WebDriver.FindElement(By.Name("password")); }
        }

        public IWebElement Alert
        {
            get { return WebDriver.FindElement(By.ClassName("alert-error")); }
        }

        public IWebElement Form
        {
            get { return WebDriver.FindElement(By.ClassName("form-signin")); }
        }

        public void Submit()
        {
            Form.Submit();
        }
    }
}
