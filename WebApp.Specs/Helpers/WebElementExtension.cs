using OpenQA.Selenium;

namespace WebApp.Specs
{
    public static class WebElementExtension
    {
        public static bool IsActive(this IWebElement element)
        {
            return element.GetAttribute("class").Contains("active");
        }
    }
}
