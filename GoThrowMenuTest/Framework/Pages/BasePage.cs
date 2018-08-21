using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace LiteCartTests.Framework
{
    public abstract class BasePage
    {
        protected string siteUrl;
        protected IWebDriver driver;
        protected WebDriverWait wait;

        protected bool AtWork() => System.Environment.MachineName == "MINBOPC037";
        protected string GetPort() => AtWork() ? ":8081" : "";


        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            siteUrl = $"http://localhost:8889/litecart/";
        }
    }
}