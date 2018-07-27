using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TestProject
{
    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnK")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector(".rc")));
            wait.Until(ExpectedConditions.TitleIs("webdriver - Пошук Google"));
        }

        public bool IsElementPresent(By locator)
        {
            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(locator));
                driver.FindElement(locator);
                return true;
            }
            catch (WebDriverTimeoutException ex)
            {
                return false;
            }
        }

        [TearDown]
        public void stop()
        {
            driver?.Quit();
            driver = null;
        }
    }

    [TestFixture]
    public class LoginTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            //driver = new FirefoxDriver();
            FirefoxOptions options = new FirefoxOptions();
            //options.UseLegacyImplementation = true;
            //options.BrowserExecutableLocation = @"C:\Users\a.rudakov\Downloads\firefoxsdk\bin\firefox.exe";
            options.BrowserExecutableLocation = @"c:\Program Files\Firefox Nightly\firefox.exe";
            driver = new FirefoxDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

        }

        [Test]
        public void GoThrowMenu()
        {
            driver.Url = "http://localhost:8889/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            var menu = driver.FindElements(By.CssSelector("ul#box-apps-menu li#app-"));
            int i = 0;
            foreach (var pmenu in menu)
            {
                driver.FindElements(By.CssSelector("ul#box-apps-menu li#app-"))[i].Click();
                i++;
                int elements = driver.FindElements(By.CssSelector("ul#box-apps-menu li#app- li")).Count;

                for (int j = 1; j < elements; j++)
                {
                    Assert.That(driver.FindElements(By.CssSelector("td#content h1")).Count, Is.EqualTo(1));
                    driver.FindElements(By.CssSelector("ul#box-apps-menu li#app- li"))[j].Click();
                    Assert.That(driver.FindElements(By.CssSelector("td#content h1")).Count, Is.EqualTo(1));
                }
            }
        }
        [TearDown]
        public void stop()
        {
            driver?.Quit();
            driver = null;
        }

        [TestFixture]
        public class CheckStickerTest
        {
            private IWebDriver driver;
            private WebDriverWait wait;

            [SetUp]
            public void start()
            {
                //driver = new FirefoxDriver();
                FirefoxOptions options = new FirefoxOptions();
                //options.UseLegacyImplementation = true;
                //options.BrowserExecutableLocation = @"C:\Users\a.rudakov\Downloads\firefoxsdk\bin\firefox.exe";
                options.BrowserExecutableLocation = @"c:\Program Files\Firefox Nightly\firefox.exe";
                driver = new FirefoxDriver(options);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            }

            [Test]
            public void CheckStickerTests()
            {
                driver.Url = "http://localhost:8889/litecart/";
                var mostPopulars = driver.FindElements(By.CssSelector("#box-most-popular .product"));
                foreach (var box in mostPopulars)
                {
                    Assert.That(box.FindElements(By.CssSelector(".sticker")).Count, Is.EqualTo(1), "Найдено больше либо меньше 1 стикера в блоке Most popular");
                }
                var Campaigns = driver.FindElements(By.CssSelector("#box-most-popular .product"));
                foreach (var box in Campaigns)
                {
                    Assert.That(box.FindElements(By.CssSelector(".sticker")).Count, Is.EqualTo(1), "Найдено больше либо меньше 1 стикера в блоке Campaigns");
                }
                var latestProducts = driver.FindElements(By.CssSelector("#box-latest-products"));
                foreach (var box in latestProducts)
                {
                    Assert.That(box.FindElements(By.CssSelector(".sticker")).Count, Is.EqualTo(1), "Найдено больше либо меньше 1 стикера в блоке Campaigns");
                }

            }
            [TearDown]
            public void stop()
            {
                driver?.Quit();
                driver = null;
            }
        }
    }
}