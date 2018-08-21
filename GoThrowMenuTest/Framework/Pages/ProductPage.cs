using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace LiteCartTests.Framework
{
    public class ProductPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "options[Size]")]
        private IWebElement sizeSelect;

        [FindsBy(How = How.Name, Using = "add_cart_product")]
        private IWebElement addToCartButton;

        [FindsBy(How = How.CssSelector, Using = "#cart .quantity")]
        private IWebElement cartQuantity;

        [FindsBy(How = How.CssSelector, Using = "[title='Home']")]
        private IWebElement homeButton;

        public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public ProductPage AddToCart()
        {
            HandleSelectSize();
            int itemsInCart = CartQuantity();
            addToCartButton.Click();
            WaitForCartUpdated(itemsInCart);

            return this;
        }

        public StorePage OpenStorePage()
        {
            homeButton.Click();
            return new StorePage(driver);
        }

        private void HandleSelectSize()
        {
            try
            {
                new SelectElement(sizeSelect).SelectByIndex(2);
            }
            catch (NoSuchElementException)
            { }
        }

        private int CartQuantity() => Int32.Parse(cartQuantity.Text);

        private void WaitForCartUpdated(int quantity)
        {
            wait.Until(d => CartQuantity() > quantity);
        }
    }
}