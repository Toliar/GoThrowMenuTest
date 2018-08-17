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
using System.Collections.ObjectModel;

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
                List<string> geoZoneList = new List<string>();
                List<string> geoZones = new List<string>();

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

                driver.Url = "http://localhost:8889/litecart/admin/?app=geo_zones&doc=geo_zones";
                var rows4 = driver.FindElements(By.CssSelector("table .row"));
                foreach (var row4 in rows4)
                    geoZones.Add(row4.FindElement(By.CssSelector("a")).GetAttribute("href"));

                for (int j = 0; j < geoZones.Count; j++)
                {
                    driver.Url = geoZones[j];
                    var rows3 = driver.FindElements(By.CssSelector("form #table-zones tr:not(.header)"));
                    for (int i = 0; i < rows3.Count - 1; i++)
                    {
                        geoZoneList.Add(rows3[i].FindElements(By.CssSelector("td"))[2].FindElement(By.CssSelector("[selected='selected']")).GetAttribute("textContent"));
                    }
                    // geoZoneList.RemoveAt(geoZones.Count);

                    Assert.That(geoZoneList, Is.Ordered, "Лист зон по ссылке " + geoZones[j].ToString() + " не отсортирован");
                    geoZoneList.Clear();
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

                mainPageProduct.Name = firstCampaignsBox.FindElement(By.CssSelector(".name")).GetAttribute("textContent");
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

                pageProduct.Name = driver.FindElement(By.CssSelector("#box-product h1")).GetAttribute("textContent");
                pageProduct.RegularPrice = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetAttribute("textContent");
                pageProduct.CampaignPrice = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetAttribute("textContent");
                pageProduct.regularPriceColor = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetCssValue("color");
                pageProduct.CampaignPriceColor = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetCssValue("color");
                pageProduct.RegularPriceFontSize = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetCssValue("font-size");
                pageProduct.CampaignPriceFontSize = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetCssValue("font-size");
                pageProduct.TagRegularPrice = informationBox.FindElement(By.CssSelector(regularPriceLocator)).GetAttribute("tagName");
                pageProduct.TagCampaignPrice = informationBox.FindElement(By.CssSelector(campaignPriceLocator)).GetAttribute("tagName");

                Assert.That(mainPageProduct.Name, Is.EqualTo(pageProduct.Name), "Имена продукта отличается");

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

    [TestFixture]
    public class Regisratration
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [SetUp]
        public void start()
        {

             FirefoxOptions options = new FirefoxOptions();
                options.UseLegacyImplementation = true;
            
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
        [Test]
        public void RegisratrationTest()
        {

            UserData User = new UserData() { FName = RandomString(3), LName = RandomString(5), Address = RandomString(10), City = RandomString(5), Password = RandomString(6) };
            User.Country = "United States";
            User.Phone = "123451";
            User.Postcode = "12345";
            User.Email = RandomString(8) + User.Email;

            driver.Url = "http://localhost:8889/litecart/en/";
            driver.FindElement(By.CssSelector("[name='login_form'] a")).Click();
            var catable = driver.FindElement(By.CssSelector("[name = 'customer_form'] tbody"));

            catable.FindElement(By.CssSelector("[name='firstname']")).SendKeys(User.FName);
            catable.FindElement(By.CssSelector("[name='lastname']")).SendKeys(User.LName);
            catable.FindElement(By.CssSelector("[name='address1']")).SendKeys(User.Address);
            catable.FindElement(By.CssSelector("[name='postcode']")).SendKeys(User.Postcode);
            catable.FindElement(By.CssSelector("[name='city']")).SendKeys(User.City);
            catable.FindElement(By.CssSelector("[name='email']")).SendKeys(User.Email);
            catable.FindElement(By.CssSelector("[name='phone']")).SendKeys(User.Phone);
            catable.FindElement(By.CssSelector("[name='password']")).SendKeys(User.Password);
            catable.FindElement(By.CssSelector("[name='confirmed_password']")).SendKeys(User.Password);

            IWebElement Country = catable.FindElement(By.CssSelector("[name = 'country_code']"));
            SelectElement selectCountry = new SelectElement(Country);
            selectCountry.SelectByText(User.Country);

            driver.FindElement(By.CssSelector("[name=create_account]")).Click();

            //   Логаут
            driver.FindElement(By.CssSelector(".left a[href*=logout]")).Click();

            driver.FindElement(By.CssSelector("[name=email]")).SendKeys(User.Email);
            driver.FindElement(By.CssSelector("[name=password]")).SendKeys(User.Password);
            driver.FindElement(By.CssSelector("[name='login_form'] [value='Login']")).Click();

            //   Логаут
            driver.FindElement(By.CssSelector(".left a[href*=logout]")).Click();

        }


        [TearDown]
        public void stop()
        {
            driver?.Quit();
            driver = null;
        }
    }

    [TestFixture]
    public class CreateItem
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [SetUp]
        public void start()
        {

            // FirefoxOptions options = new FirefoxOptions();
            //      options.UseLegacyImplementation = true;
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }
        [Test]
        public void CreateItemTest()
        {
            ProductData product = new ProductData();
            product.Name = RandomString(3) + " duck";
            product.Code = "123";
            product.RegularPrice = "123";
            product.Keywords = RandomString(4) + RandomString(4) + RandomString(4);
            product.Description = RandomString(4);



            driver.Url = "http://localhost:8889/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.FindElements(By.CssSelector(".name"))[1].Click();
            driver.FindElements(By.CssSelector("#content div a"))[1].Click();

            var general = driver.FindElement(By.CssSelector(".content #tab-general"));
            general.FindElement(By.CssSelector("[name='name[en]']")).SendKeys(product.Name);
            general.FindElement(By.CssSelector("[name='code']")).SendKeys(product.Code);
            general.FindElement(By.CssSelector("input[type=file]")).SendKeys(Environment.CurrentDirectory + product.Image);

            var info = driver.FindElement(By.CssSelector("a[href*=information]"));
            info.Click();
            info = driver.FindElement(By.CssSelector("div#tab-information"));
            SelectElement selectManufacter = new SelectElement(info.FindElement(By.CssSelector("[name=manufacturer_id]")));
            selectManufacter.SelectByValue("1");
            info.FindElement(By.CssSelector("[name=keywords]")).SendKeys(product.Keywords);
            info.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys(product.Description);

            var pricesTab = driver.FindElement(By.CssSelector("#content .index a[href*=prices]"));
            pricesTab.Click();

            var pricesTabContent = driver.FindElement(By.CssSelector("div#tab-prices"));
            var price = pricesTabContent.FindElement(By.CssSelector("input[name=purchase_price]"));
            price.Clear();
            price.SendKeys(product.RegularPrice);
            new SelectElement(pricesTabContent.FindElement(By.CssSelector("select[name=purchase_price_currency_code]"))).SelectByValue("USD");
            // Сохраняем товар
            driver.FindElement(By.CssSelector("button[name=save]")).Click();
            // Проверяем, что отображается в админке
            var createdProduct = driver.FindElement(By.XPath($"//form[@name='catalog_form']//a[text()='{product.Name}']"));
            Assert.That(createdProduct.Displayed, Is.True, "Не отображается созданный товар!");

        }
        [TearDown]
        public void stop()
        {
            driver?.Quit();
            driver = null;
        }

    }

    public class Cart
    {
        IWebDriver driver;
        private WebDriverWait wait;
        string mainPageUrl = "http://localhost:8889/litecart/";
        [SetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [Test]
        public void CartTest()
        {
            driver.Navigate().GoToUrl(mainPageUrl);
            // Добавляем 3 товара в корзину
            for (int i = 0, j = 1; i < 3; i++)
            {
                driver.FindElement(By.CssSelector(".content .product:first-of-type")).Click();
                if (driver.FindElements(By.CssSelector("select[name='options[Size]']")).Count > 0)
                    new SelectElement(driver.FindElement(By.CssSelector("select[name='options[Size]']"))).SelectByIndex(1);
                driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
                var cartItemsCount = driver.FindElement(By.CssSelector("div#cart .quantity"));
                wait.Until(webDriver => cartItemsCount.Text.Equals($"{j}"));
                driver.FindElement(By.CssSelector("#logotype-wrapper")).Click();
                j++;
            }
            driver.FindElement(By.CssSelector("#cart a.link")).Click();
            // Удаляем 3 товара из корзины
            var totalCartItems = driver.FindElements(By.CssSelector("#box-checkout-cart li.item")).Count;
            for (int i = 0; i < totalCartItems; i++)
            {
                var totalItemsInCartBefore = driver.FindElements(By.CssSelector("#order_confirmation-wrapper tr:not(.header) .item")).Count;
                driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();
                wait.Until(webDriver => webDriver.FindElements(By.CssSelector("#order_confirmation-wrapper tr:not(.header) .item")).Count < totalItemsInCartBefore);
            }
        }
        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
            driver = null;
        }
    }



        public class LinksOpenedInNewWindow
        {
            IWebDriver driver;
            private WebDriverWait wait;
            string adminUrl = "http://localhost:8889/litecart/admin/";
            [SetUp]
            public void BeforeTest()
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }
            [Test]
            public void LinksOpenedInNewWindowTest()
            {
                driver.Navigate().GoToUrl(adminUrl);
                driver.FindElement(By.CssSelector("span input[name='username']")).SendKeys("admin");
                driver.FindElement(By.CssSelector("span input[name='password']")).SendKeys("admin");
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                driver.Navigate().GoToUrl("http://localhost:8889/litecart/admin/?app=countries&doc=countries");
                driver.FindElement(By.CssSelector("a.button[href*=edit_country]")).Click();
                var links = driver.FindElements(By.CssSelector("#content td a:not([href='#'])"));
                foreach (var lnk in links)
                {
                    var windowsBefore = driver.WindowHandles;
                    var currentWindow = driver.CurrentWindowHandle;
                    lnk.Click();
                    var newWindow = wait.Until(d => anyWindowOtherThan(windowsBefore));
                    driver.SwitchTo().Window(newWindow);
                    Assert.That(driver.Title, Is.Not.Null.Or.Empty, "Новое окно не открылось!");
                    driver.Close();
                    driver.SwitchTo().Window(currentWindow);
                }
            }
            [TearDown]
            public void AfterTest()
            {
                driver.Quit();
                driver = null;
            }
            public string anyWindowOtherThan(ReadOnlyCollection<string> oldWindows)
            {
                wait.Until(d => d.WindowHandles.ToList().Count > oldWindows.Count);
                var handles = driver.WindowHandles.ToList();
                var newWindow = handles.Except(oldWindows).First();
                return newWindow.Any() ? newWindow : null;
            }
        }
    }