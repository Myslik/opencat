using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using OpenQA.Selenium;
using WebApp.Specs.Pages;

namespace WebApp.Specs
{
    public abstract class WebStepsBase
    {
        protected IWebDriver WebDriver
        {
            get { return BasePage.WebDriver; }
        }

        protected string Site
        {
            get { return BasePage.Site; }
        }

        protected string FileFolder
        {
            get
            {
                var path = new DirectoryInfo(Environment.CurrentDirectory);
                return Path.Combine(path.Parent.Parent.FullName, "Files");
            }
        }

        protected string File(string filename)
        {
            return Path.Combine(FileFolder, filename);
        }

        protected void GoHome()
        {
            WebDriver.Navigate().GoToUrl(Site);
        }

        private Dictionary<Type, BasePage> pages = new Dictionary<Type,BasePage>();

        protected T On<T>() where T : BasePage
        {
            if (!pages.ContainsKey(typeof(T)))
            {
                pages.Add(typeof(T), Activator.CreateInstance(typeof(T)) as BasePage);
            }
            return pages[typeof(T)] as T;
        }

        protected T On<T>(Action<T> action) where T : BasePage
        {
            var page = On<T>();
            action.Invoke(page);
            return page;
        }
    }
}
