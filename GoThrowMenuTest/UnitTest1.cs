using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

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
                var Campaigns = driver.FindElements(By.CssSelector("#box-campaigns .product"));
                foreach (var box in Campaigns)
                {
                    Assert.That(box.FindElements(By.CssSelector(".sticker")).Count, Is.EqualTo(1), "Найдено больше либо меньше 1 стикера в блоке Campaigns");
                }
                var latestProducts = driver.FindElements(By.CssSelector("#box-latest-products .product"));
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

        [TestFixture]
        public class CheckCountrySorting
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
                //     options.BrowserExecutableLocation = @"c:\Program Files\Firefox Nightly\firefox.exe";
                //    driver = new FirefoxDriver(options);

                driver = new ChromeDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            }

            [Test]
            public void CheckCountrySortingTest()
            {
                var countryList = new List<string>();
                List<string> timeZoneList = new List<string>();
                List<string> timeZonesForCheck = new List<string>();

                driver.Url = "http://localhost:8889/litecart/admin/?app=countries&doc=countries";
                driver.FindElement(By.Name("username")).SendKeys("admin");
                driver.FindElement(By.Name("password")).SendKeys("admin");
                driver.FindElement(By.Name("login")).Click();

                var rows = driver.FindElements(By.CssSelector("form .dataTable .row"));
                foreach (var row in rows)
                {
                    countryList.Add(row.FindElements(By.CssSelector("td"))[4].Text);
                    if (row.FindElements(By.CssSelector("td"))[5].Text != "0")
                    {
                        timeZonesForCheck.Add(row.FindElement(By.CssSelector("a")).GetAttribute("href"));
                    }

                }
                Assert.That(countryList, Is.Ordered, "Лист не отсортирован");

                for (int j = 0; j < timeZonesForCheck.Count; j++)
                {
                    driver.Url = timeZonesForCheck[j];
                    var rows2 = driver.FindElements(By.CssSelector("form #table-zones tr:not(.header)"));
                    foreach (var row2 in rows2)
                    {
                        timeZoneList.Add(row2.FindElements(By.CssSelector("td"))[2].GetAttribute("outerText"));
                    }
                    timeZoneList.RemoveAt(timeZoneList.Count - 1);

                    Assert.That(timeZoneList, Is.Ordered, "Лист зон по ссылке " + timeZonesForCheck[j].ToString() + " не отсортирован");
                    timeZoneList.Clear();
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
        public class CheckFirstCardItem
        {
            private IWebDriver driver;
            private WebDriverWait wait;

            [SetUp]
            public void start()
            {
                FirefoxOptions options = new FirefoxOptions();
                options.UseLegacyImplementation = true;
                driver = new FirefoxDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            }

            [Test]
            public void CheckFirstCardItemTest()
            {
                ProductData mainPageProduct = new ProductData();
                ProductData pageProduct = new ProductData();
                driver.Url = "http://localhost:8889/litecart/en/";
                var firstCampaignsBox = driver.FindElements(By.CssSelector("#box-campaigns .product"))[0];
                var regularPriceLocator = ".regular-price";
                var campaignPriceLocator = ".campaign-price";

                mainPageProduct.ProductName = firstCampaignsBox.FindElement(By.CssSelector(".name")).GetAttribute("textContent");
                mainPageProduct.RegularPrice = firstCampaignsBox.FindElement(By.CssSelector(regularPriceLocator)).GetAttribute("textContent");
                mainPageProduct.CampaignPrice = firstCampaignsBox.FindElement(By.CssSelector(campaignPriceLocator)).GetAttribute("textContent");
                mainPageProduct.regularPriceColor = firstCampaignsBox.FindElement(By.CssSelector(regularPriceLocator)).GetCssValue("color");
                mainPageProduct.CampaignPriceColor = firstCampaignsBox.FindElement(By.CssSelector(campaignPriceLocator)).GetCssValue("color");
                mainPageProduct.RegularPriceFontSize = firstCampaignsBox.FindElement(By.CssSelector(regularPriceLocator)).GetCssValue("font-size");
                mainPageProduct.CampaignPriceFontSize = firstCampaignsBox.FindElement(By.CssSelector(campaignPriceLocator)).GetCssValue("font-size");
                mainPageProduct.TagRegularPrice = firstCampaignsBox.FindElement(By.CssSelector(regularPriceLocator)).GetAttribute("tagName");
                mainPageProduct.TagCampaignPrice = firstCampaignsBox.FindElement(By.CssSelector(campaignPriceLocator)).GetAttribute("tagName");

                firstCampaignsBox.Click();

                var informationBox = driver.FindElement(By.CssSelector(".content .information"));

                pageProduct.ProductName = driver.FindElement(By.CssSelector("#box-product h1")).GetAttribute("textContent");
                pageProduct.RegularPrice = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetAttribute("textContent");
                pageProduct.CampaignPrice = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetAttribute("textContent");
                pageProduct.regularPriceColor = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetCssValue("color");
                pageProduct.CampaignPriceColor = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetCssValue("color");
                pageProduct.RegularPriceFontSize = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetCssValue("font-size");
                pageProduct.CampaignPriceFontSize = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetCssValue("font-size");
                pageProduct.TagRegularPrice = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetAttribute("tagName");
                pageProduct.TagCampaignPrice = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetAttribute("tagName");

                Assert.That(mainPageProduct.ProductName, Is.EqualTo(pageProduct.ProductName), "Имена продукта отличается");

                Assert.That(mainPageProduct.RegularPrice, Is.EqualTo(pageProduct.RegularPrice), "обычная цена продукта отличается");
                Assert.That(mainPageProduct.CampaignPrice, Is.EqualTo(pageProduct.CampaignPrice), "обычная цена продукта отличается");

                Assert.That(mainPageProduct.RegularPriceFontSize, Is.LessThan(mainPageProduct.CampaignPriceFontSize), "Цена не больше на главной");
                Assert.That(pageProduct.RegularPriceFontSize, Is.LessThan(pageProduct.CampaignPriceFontSize), "Цена не больше на странице продукта");

                Assert.That(mainPageProduct.TagRegularPrice, Is.EqualTo("S"), "Обычная цена не зачеркнута на главной");
                Assert.That(mainPageProduct.TagRegularPrice, Is.EqualTo("S"), "Обычная цена не зачеркнута на странице продукта");

                Assert.That(RGBChannels(mainPageProduct.regularPriceColor)[0], Is.EqualTo(RGBChannels(mainPageProduct.regularPriceColor)[1]).And.EqualTo(RGBChannels(mainPageProduct.regularPriceColor)[2]), "Каналы цвета не одинаковые");
                Assert.That(RGBChannels(mainPageProduct.CampaignPriceColor)[1], Is.EqualTo("0").And.EqualTo(RGBChannels(mainPageProduct.CampaignPriceColor)[2]), "Акционная цена цвет G B не 0");


                Assert.That(RGBChannels(pageProduct.regularPriceColor)[0], Is.EqualTo(RGBChannels(pageProduct.regularPriceColor)[1]).And.EqualTo(RGBChannels(pageProduct.regularPriceColor)[2]), "Каналы цвета не одинаковые на странице продукта");
                Assert.That(RGBChannels(pageProduct.CampaignPriceColor)[1], Is.EqualTo("0").And.EqualTo(RGBChannels(pageProduct.CampaignPriceColor)[2]), "Акционная цена цвет G B не 0 на странице продукта");

                Assert.That(mainPageProduct.TagCampaignPrice, Is.EqualTo("STRONG"), "Акционная цена не выделена жирным на странице главной");
                Assert.That(pageProduct.TagCampaignPrice, Is.EqualTo("STRONG"), "Акционная цена не выделена жирным на странице продукта");
            }




                public string[] RGBChannels(string RGB)
                {
                var RGBChannel = Regex.Matches(RGB, @"\d+");
                string[] rgb = { RGBChannel[0].ToString(), RGBChannel[1].ToString(), RGBChannel[2].ToString() };
                return rgb;
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