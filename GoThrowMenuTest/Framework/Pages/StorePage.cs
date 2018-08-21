using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LiteCartTests.Framework
{
    public class StorePage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "#box-most-popular .product:first-child")]
        private IWebElement firstPopularProduct;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkoutButton;

        public StorePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public StorePage OpenStorePage()
        {
            driver.Navigate().GoToUrl(siteUrl);

            return this;
        }

        public CartPage OpenCartPage()
        {
            checkoutButton.Click();

            return new CartPage(driver);
        }

        public ProductPage OpenFirstPopularProduct()
        {
            firstPopularProduct.Click();

            return new ProductPage(driver);
        }

    }
}