using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LiteCartTests.Framework
{
    public class AdminLoginPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "username")]
        private IWebElement usernameInput;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement passwordInput;

        [FindsBy(How = How.Name, Using = "login")]
        private IWebElement loginButton;

        private const string adminLogin = "admin";
        private const string adminPassword = "admin";
        private string siteAdminUrl;

        public AdminLoginPage(IWebDriver driver) : base(driver)
        {
            siteAdminUrl = siteUrl + "admin/";
            PageFactory.InitElements(driver, this);
        }

        public AdminLoginPage Open()
        {
            driver.Navigate().GoToUrl(siteAdminUrl);
            return this;
        }

        public void LoginAsAdmin()
        {
            usernameInput.SendKeys(adminLogin);
            passwordInput.SendKeys(adminPassword);
            loginButton.Click();
        }
    }
}